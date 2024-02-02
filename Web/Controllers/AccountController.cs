using Application.Common.DTOs;
using Application.UseCases.Identity.Commands;
using Application.UseCases.Identity.Queries;
using Application.UseCases.Tasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    public readonly ISender sender;
    public readonly IConfiguration configuraion;
    public AccountController(ISender _sender, IConfiguration _configuraion)
    {
        sender = _sender;
        configuraion = _configuraion;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<string>> Register(RegisterCommand comand)
    {
        var result = await sender.Send(comand);

        return result.Match<ActionResult<string>>(
            res => Ok(res),
            ex => BadRequest(ex.Message)
            );
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<string>> Login(LoginPasswordQuery query)
    {
        var result = await sender.Send(query);

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

        var loginResult = await sender.Send(new LoginExternalQuery()
        {
            Username = username,
            Email = email
        });

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
