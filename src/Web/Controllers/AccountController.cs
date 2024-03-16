using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Models;
using Web.Models.Identity;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IConfiguration _configuraion;
    public AccountController(IIdentityService identityService, IConfiguration configuraion)
    {
        _identityService = identityService;
        _configuraion = configuraion;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterDto request)
    {
        var result = await _identityService.RegisterAsync(request.UserName, request.Email, request.Password);

        return result.Match<ActionResult<string>>(
        res => Ok(res),
        ex => BadRequest(ex.Message)
        );
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto request)
    {
        var result = await _identityService.LoginPasswordAsync(request.Email, request.Password);

        return result.Match<ActionResult<string>>(
            res => Ok(res),
            ex => BadRequest(ex.Message)
            );
    }

    [HttpGet]
    [Route("GoogleLogin")]
    public ActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("googleResponse")]
    public async Task<ActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (result == null || !result.Succeeded) return BadRequest("Login failed");

        var username = result.Principal.Identities.First().Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        var email = result.Principal.Identities.First().Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

        var loginResult = await _identityService.LoginExternalAsync(username, email);

        return loginResult.Match<ActionResult>(
            res =>
            {
                HttpContext.Response.Cookies.Append("accessToken", res, new CookieOptions { IsEssential = true });

                string url = _configuraion["JwtSettings:Audience"];

                return Redirect(url);
            },
            ex => BadRequest(ex.Message)
            );
    }
}
