﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.version.controller;

[Route("api/v{version:apiVersion}/teste")]
[ApiController]
[ApiVersion("1.0", Deprecated = true)]
public class TesteV1Controller : ControllerBase
{
    [HttpGet]
    public string GetVersion()
    {
        return "TesteV1 - GET - API Version 1.0";
    }
}