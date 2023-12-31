using Azure;
using FastFoodShop.Services.AuthAPI.Models.Dto;
using FastFoodShop.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodShop.Services.AuthAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
    {
        string errorMessage = await _authService.Register(request);
        
        if (!errorMessage.IsNullOrEmpty())
            return BadRequest(errorMessage);
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        LoginResponseDto loginResponse = await _authService.Login(request);

        ResponseDto _response = new ResponseDto();
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return BadRequest(_response);
        }

        _response.Result = loginResponse;
        return Ok(_response);
    }
}