using Application.Common.DTOs;
using Application.UseCases.Users.Queries;
using Application.UseCases.Workspaces.Commands;
using Application.UseCases.Workspaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models.Workspace;

namespace Web.Controllers;
[Authorize]
public class WorkspaceController : BaseApiController
{
    private readonly ISender sender;
    public WorkspaceController(ISender _sender)
    {
        sender = _sender;
    }

    [HttpGet]
    [Route("GetWorkspaces")]
    public async Task<ActionResult<List<WorkspaceDto>>> GetWorkspaces()
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new GetWorkspacesQuery()
        {
            UserId = userId
        });

        return HandleResult(result);
    }

    [HttpGet]
    [Route("GetWorkspaceMembers")]
    public async Task<ActionResult<List<UserMembershipDto>>> GetWorkspaceMembers(int workspaceId)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new GetWorkspaceUsersQuery()
        {
            WorkspaceId = workspaceId,
            UserId = userId
        });

        return HandleResult(result);
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
            WorkspaceName = request.WorkspaceName,
            WorkspaceIsPublic = request.WorkspaceIsPublic
        });

        return HandleResult(result);
    }
    
    [HttpPost]
    [Route("UpdateWorkspace")]
    public async Task<ActionResult> UpdateWorkspace([FromBody]UpdateWorkspaceDto request)
    {
        var userId = User.GetUserId();
        
        if (userId == 0) return Unauthorized();
        
        var result = await sender.Send(new UpdateWorkspaceCommand()
        {
            UserId = userId,
            WorkspaceId = request.WorkspaceId,
            WorkspaceName = request.WorkspaceName,
            WorkspaceIsPublic = request.WorkspaceIsPublic
        });

        return HandleResult(result);
    }
    
    [HttpPost]
    [Route("DeleteWorkspace")]
    public async Task<ActionResult> DeleteWorkspace([FromBody]DeleteWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new DeleteWorkspaceCommand()
        {
            UserId = userId,
            WorkspaceId = request.WorkspaceId
        });

        return HandleResult(result);
    }
    
    [HttpPost]
    [Route("AddtoWorkspace")]
    public async Task<ActionResult> AddtoWorkspace([FromBody]AddToWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new AddToWorkspaceCommand()
        {
            UserId = userId,
            WorkspaceId = request.MembershipId,
            ToAddUserId = request.ToAddUserId,
            ToAddRoleId = request.ToAddRoleId
        });

        return HandleResult(result);
    }
    
    [HttpPost]
    [Route("RemoveFromWorkspace")]
    public async Task<ActionResult> RemoveFromWorkspace([FromBody]RemoveFromWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new RemoveFromWorkspaceCommand()
        {
            UserId = userId,
            MembershipId = request.MembershipId,
            ToRemoveMembershipId = request.ToRemoveMembershipId
        });

        return HandleResult(result);
    }
}