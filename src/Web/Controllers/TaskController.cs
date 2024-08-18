using Application.Common.DTOs;
using Application.UseCases.Tasks.Commands;
using Application.UseCases.Tasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models.Task;

namespace Web.Controllers;
public class TaskController : BaseApiController
{
    private readonly ISender sender;
    public TaskController(ISender _sender)
    {
        sender = _sender;
    }

    [HttpGet]
    [Route("Tasks")]
    public async Task<ActionResult<List<TaskDto>>> GetTasks(int WorkspaceId)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new GetWorkspaceTasksQuery()
        {
            UserId = userId,
            WorkspaceId = WorkspaceId
        });

        return HandleResult(result);
    }

    [HttpPost]
    [Route("CreateTask")]
    public async Task<ActionResult<int>> CreateTask([FromBody] CreateTaskDto command)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new CreateTaskCommand()
        {
            UserId = userId,
            WorkspaceId = command.WorkspaceId,
            Title = command.Title,
            Description = command.Description,
            StartDate = command.StartDate,
            EndDate = command.EndDate
        });

        return HandleResult(result);
    }

    [HttpPut]
    [Route("UpdateTask")]
    public async Task<ActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new UpdateTaskCommand()
        {
            Id = command.Id,
            Title = command.Title,
            Description = command.Description,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            UserId = userId
        });

        return HandleResult(result);
    }


    [HttpDelete]
    [Route("DeleteTask")]
    public async Task<ActionResult> DeleteTask([FromBody] DeleteTaskDto command)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new DeleteTaskCommand()
        {
            Id = command.Id,
            UserId = userId
        });

        return HandleResult(result);
    }
}
