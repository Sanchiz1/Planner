using Application.Common.DTOs;
using Application.UseCases;
using Application.UseCases.Tasks.Commands;
using Application.UseCases.Tasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Extensions;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : Controller
{
    private readonly ISender sender;
    public TaskController(ISender _sender)
    {
        sender = _sender;
    }

    [HttpGet]
    [Route("Tasks")]
    public async Task<ActionResult<List<TaskDto>>> GetTasks()
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();
        
        var result = await sender.Send(new GetTasksQuery() { UserId = userId });

        return result.Match<ActionResult<List<TaskDto>>>(
            res => res,
            ex => BadRequest(ex.Message)
            );
    }

    [HttpPost]
    [Route("CreateTask")]
    public async Task<ActionResult<int>> CreateTask(CreateTaskCommand command)
    {
        var result = await sender.Send(command);

        return result.Match<ActionResult<int>>(
            res => res,
            ex => BadRequest(ex.Message)
            );
    }
}
