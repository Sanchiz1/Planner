using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    public readonly IIdentityService identityService;
    public readonly IConfiguration configuraion;
    public AccountController(IIdentityService _identityService, IConfiguration _configuraion)
    {
        identityService = _identityService;
        configuraion = _configuraion;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<string>> Register(string userName, string email, string password)
    {
        var result = await identityService.RegisterAsync(userName, email, password);

        return result.Match<ActionResult<string>>(
        res => Ok(res),
        ex => BadRequest(ex.Message)
        );
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<string>> Login(string email , string password)
    {
        var result = await identityService.LoginPasswordAsync(email, password);

        return result.Match<ActionResult<string>>(
            res => Ok(res),
            ex => BadRequest(ex.Message)
            );
    }

    [HttpGet]
    [Route("google-login")]
    public ActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("google-response")]
    public async Task<ActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (result == null || !result.Succeeded) return BadRequest("Login failed");

        var username = result.Principal.Identities.First().Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        var email = result.Principal.Identities.First().Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

        var loginResult = await identityService.LoginExternalAsync(username, email);

        return loginResult.Match<ActionResult>(
            res =>
            {
                HttpContext.Response.Cookies.Append("accessToken", res, new CookieOptions { IsEssential = true });

                string url = configuraion["JwtSettings:Audience"];

                return Redirect(url);
            },
            ex => BadRequest(ex.Message)
            );
    }
}
