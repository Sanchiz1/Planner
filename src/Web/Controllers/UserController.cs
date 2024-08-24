using Application.Common.DTOs;
using Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models.User;

namespace Web.Controllers;

public class UserController : BaseApiController
{
    private readonly ISender sender;
    public UserController(ISender _sender)
    {
        sender = _sender;
    }

    [HttpGet]
    [Route("email/{email}")]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var result = await sender.Send(new GetUserByEmailQuery()
        {
            Email = email
        });

        return HandleResult(result);
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<UserDto>> GetUsers([FromQuery] UserQueryParameters query)
    {
        var result = await sender.Send(new GetUsersQuery()
        {
            Email = query.Email,
            Page = query.Page,
            Size = query.Size,
        });

        return HandleResult(result);
    }
}
