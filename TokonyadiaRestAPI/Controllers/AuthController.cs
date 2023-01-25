using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Services;

namespace TokonyadiaRestAPI.Controllers;

[Route("api/auth")]
public class AuthController:BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var user = await _authService.Register(request,Request.Path.Value);
        return Ok(user);
    }
    
    
    [HttpPost("register-admin")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAdmin([FromBody] AuthRequest request)
    {
        var user = await _authService.Register(request,Request.Path.Value);
        return Ok(user);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var user = await _authService.Login(request);
        return Ok(user);
    }

}