using Application.Common.DTOs;
using Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class UserController : BaseApiController
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

        return HandleResult(result);
    }
}
