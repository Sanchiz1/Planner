using Application.Common.DTOs;
using Application.UseCases.Tasks.Commands;
using Application.UseCases.Tasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TaskController : Controller
{
    public readonly ISender sender;
    public TaskController(ISender _sender)
    {
        sender = _sender;
    }

    [HttpGet]
    [Route("Tasks")]
    public async Task<ActionResult<List<TaskDto>>> GetTasks()
    {
        var result = await sender.Send(new GetTasksQuery());

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
