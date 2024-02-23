using Application.Common.DTOs;
using Application.UseCases.Users.Queries;
using Application.UseCases.Workspaces.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models.Workspace;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly ISender sender;
    public UserController(ISender _sender)
    {
        sender = _sender;
    }

    [HttpGet]
    [Route("GetUser")]
    public async Task<ActionResult<UserDto>> GetUser(string email)
    {
        var result = await sender.Send(new GetUserByEmailQuery()
        {
            Email = email
        });

        return result.Match<ActionResult<UserDto>>(
            res => res,
            ex => BadRequest(ex.Message)
        );
    }
}
