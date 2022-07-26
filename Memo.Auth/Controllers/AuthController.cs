﻿using Memo.Auth.Attributes;
using Memo.Auth.Interfaces;
using Memo.Auth.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Memo.Auth.Controllers;

/// <summary>Контроллер авторизации</summary>
[Route("api/[controller]/[action]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>Выполнение входа пользователя</summary>
    [ValidateModel]
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestModel requestModel)
    {
        var token = await _authService.LoginAsync(requestModel);
        
        if (! string.IsNullOrWhiteSpace(token))
            return Ok(token);

        return Unauthorized();
    }
    
    /// <summary>Регистрация ползователя</summary>
    [ValidateModel]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestModel requestModel)
    {
        var token = await _authService.RegisterAsync(requestModel);
        
        if (! string.IsNullOrWhiteSpace(token))
            return Ok(token);

        return BadRequest();
    }
}