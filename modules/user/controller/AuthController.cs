﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api_catalogo_curso.modules.token.models.request;
using api_catalogo_curso.modules.token.service.interfaces;
using api_catalogo_curso.modules.user.models.entity;
using api_catalogo_curso.modules.user.models.request;
using api_catalogo_curso.modules.user.models.response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.user.controller;

//TODO: REFATORAR CODIGO 
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration,
        ILogger<AuthController> logger)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims,
                _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime =
                DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            // Converter DateTime para UTC antes de salvar    
            user.RefreshTokenExpiryTime = user.RefreshTokenExpiryTime.ToUniversalTime();

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });
        }

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User creation failed." });
        }

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenRequest? tokenModel)
    {
        if (tokenModel == null || 
            string.IsNullOrEmpty(tokenModel.AccessToken) || 
            string.IsNullOrEmpty(tokenModel.RefreshToken))
        {
            return BadRequest("Invalid client request");
        }
        
        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken!, _configuration);
        
        string? username = principal.Identity?.Name;

        var user = await _userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != tokenModel.RefreshToken
                         || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest("Invalid access token/refresh token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(
            principal.Claims.ToList(), _configuration);
        
        user.RefreshToken = _tokenService.GenerateRefreshToken();

        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = user.RefreshToken
        });
    }

    [Authorize(policy: "ADMIN")]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return BadRequest("Invalid user name");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
    
    [Authorize(policy: "MASTER")]
    [HttpPost]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        bool roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (roleResult.Succeeded)
            {
                _logger.LogInformation(1, "Roles Added");
                return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = $"Role {roleName} added successfully!" });
            }

            _logger.LogInformation(2, "Error");
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = $"Issue adding the new {roleName} role" });
        }

        return StatusCode(StatusCodes.Status400BadRequest,
            new Response { Status = "Error", Message = "Role already exist" });
    }
    
    [Authorize(policy: "MASTER")]
    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"User {user.Email} added to the {roleName} role" });
            }

            _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {roleName} role");
            return StatusCode(StatusCodes.Status400BadRequest, new Response{Status = "Error", Message = $"Error: Unable to add user {user.Email}  to the {roleName} role"});
        }
        return BadRequest(new Response { Status = "Error", Message = "Unable to find user" });
    }
    
}