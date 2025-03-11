using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.version.controller;

[Route("api/v{version:apiVersion}/teste")]
[ApiController]
[ApiVersion("2.0")]
public class TesteV2Controller : ControllerBase
{
    [HttpGet]
    public string GetVersion()
    {
        return "TesteV2 - GET - API Version 2.0";
    }
}