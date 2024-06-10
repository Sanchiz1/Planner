using Application.Common.DTOs;
using Application.UseCases;
using Application.UseCases.Tasks.Commands;
using Application.UseCases.Tasks.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Extensions;
using Web.Models.Task;

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
    public async Task<ActionResult<List<TaskDto>>> GetTasks(int WorkspaceId)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new GetWorkspaceTasksQuery()
        {
            UserId = userId,
            WorkspaceId = WorkspaceId
        });

        return result.Match<ActionResult<List<TaskDto>>>(
            res => res,
            ex => BadRequest(ex.Message)
            );
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

        return result.Match<ActionResult<int>>(
            res => res,
            ex => BadRequest(ex.Message)
            );
    }

    [HttpPut]
    [Route("UpdateTask")]
    public async Task<ActionResult<int>> UpdateTask([FromBody] UpdateTaskCommand command)
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

        return result.Match<ActionResult<int>>(
            res => res,
            ex => BadRequest(ex.Message)
            );
    }


    [HttpDelete]
    [Route("DeleteTask")]
    public async Task<ActionResult<bool>> DeleteTask([FromBody] DeleteTaskDto command)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new DeleteTaskCommand()
        {
            Id = command.Id,
            UserId = userId
        });

        return result.Match<ActionResult<bool>>(
            res => res,
            ex => BadRequest(ex.Message)
            );
    }
}
