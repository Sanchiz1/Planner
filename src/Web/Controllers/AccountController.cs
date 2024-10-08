﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Models.Identity;

namespace Web.Controllers;
public class AccountController : BaseApiController
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

        return HandleResult(result);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<Token>> Login([FromBody] LoginDto request)
    {
        var result = await _identityService.LoginPasswordAsync(request.Email, request.Password);

        return HandleResult(result);
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

        if (loginResult.IsSuccess)
        {
            string url = _configuraion["JwtSettings:Audience"];

            HttpContext.Response.Cookies.Append(
                "accessToken",
                loginResult.Value.Value,
                new CookieOptions
                {
                    IsEssential = true,
                    Expires = loginResult.Value.Expires
                }
                );

            return Redirect(url);
        }

        return HandleResult(loginResult);
    }
}
