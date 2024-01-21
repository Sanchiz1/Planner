using Application.Common.DTOs;
using Application.UseCases.Identity.Commands;
using Application.UseCases.Identity.Queries;
using Application.UseCases.Tasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    public readonly ISender sender;
    public AccountController(ISender _sender)
    {
        sender = _sender;
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
    public async Task<ActionResult<string>> Login(LoginQuery query)
    {
        var result = await sender.Send(query);

        return result.Match<ActionResult<string>>(
            res => Ok(res),
            ex => BadRequest(ex.Message)
            );
    }

    [HttpGet]
    [Route("google-login")]
    public  ActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("google-response")]
    public async Task<ActionResult<IEnumerable<Claim>>> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        //return Json(result);
        var claims = result.Principal.Identities.FirstOrDefault()
            .Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
        return Json(claims);
    }
}
