using Application.Common.DTOs;
using Application.UseCases.Workspaces.Commands;
using Application.UseCases.Workspaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models.Workspace;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkspaceController : Controller
{
    private readonly ISender sender;
    public WorkspaceController(ISender _sender)
    {
        sender = _sender;
    }
    
    [HttpPost]
    [Route("CreateWorkspace")]
    public async Task<ActionResult<int>> CreateWorkspace([FromBody]CreateWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();
        
        var result = await sender.Send(new CreateWorkspaceCommand()
        {
            UserId = userId,
            WorkspaceName = request.WorkspaceName
        });

        return result.Match<ActionResult<int>>(
            res => res,
            ex => BadRequest(ex.Message)
        );
    }
    
    [HttpPost]
    [Route("UpdateWorkspace")]
    public async Task<ActionResult<int>> UpdateWorkspace([FromBody]UpdateWorkspaceDto request)
    {
        var userId = User.GetUserId();
        
        if (userId == 0) return Unauthorized();
        
        var result = await sender.Send(new UpdateWorkspaceCommand()
        {
            UserId = userId,
            MembershipId = request.MembershipId,
            WorkspaceName = request.WorkspaceName
        });

        return result.Match<ActionResult<int>>(
            res => res,
            ex => BadRequest(ex.Message)
        );
    }
    
    [HttpPost]
    [Route("DeleteWorkspace")]
    public async Task<ActionResult<string>> DeleteWorkspace([FromBody]DeleteWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new DeleteWorkspaceCommand()
        {
            UserId = userId,
            MembershipId = request.MembershipId
        });

        return result.Match<ActionResult<string>>(
            res => res,
            ex => BadRequest(ex.Message)
        );
    }
    
    [HttpPost]
    [Route("AddtoWorkspace")]
    public async Task<ActionResult<string>> AddtoWorkspace([FromBody]AddToWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new AddToWorkspaceCommand()
        {
            UserId = userId,
            MembershipId = request.MembershipId,
            ToAddUserId = request.ToAddUserId,
            ToAddRoleId = request.ToAddRoleId
        });

        return result.Match<ActionResult<string>>(
            res => res,
            ex => BadRequest(ex.Message)
        );
    }
    
    [HttpPost]
    [Route("RemoveFromWorkspace")]
    public async Task<ActionResult<string>> RemoveFromWorkspace([FromBody]RemoveFromWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new RemoveFromWorkspaceCommand()
        {
            UserId = userId,
            MembershipId = request.MembershipId,
            ToRemoveMembershipId = request.ToRemoveMembershipId
        });

        return result.Match<ActionResult<string>>(
            res => res,
            ex => BadRequest(ex.Message)
        );
    }
    
    [HttpGet]
    [Route("GetWorkspaces")]
    public async Task<ActionResult<List<MembershipDto>>> GetWorkspaces()
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new GetWorkspacesQuery()
        {
            UserId = userId
        });

        return result.Match<ActionResult<List<MembershipDto>>>(
            res => res,
            ex => BadRequest(ex.Message)
        );
    }
}