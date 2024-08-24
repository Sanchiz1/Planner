using Application.Common.DTOs;
using Application.UseCases.Users.Queries;
using Application.UseCases.Workspaces.Commands;
using Application.UseCases.Workspaces.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Result;
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
    [Route("member")]
    public async Task<ActionResult<List<MembershipDto>>> GetMemberWorkspaces()
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new GetWorkspacesQuery()
        {
            UserId = userId
        });

        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("{workspaceId}")]
    public async Task<ActionResult<WorkspaceDto>> GetWorkspace(int workspaceId)
    {
        var userId = User.GetUserId();

        var result = await sender.Send(new GetWorkspaceQuery()
        {
            WorkspaceId = workspaceId,
            UserId = userId
        });

        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("{workspaceId}/membership")]
    public async Task<ActionResult<MembershipDto>> GetWorkspaceMembership(int workspaceId)
    {
        var userId = User.GetUserId();

        var result = await sender.Send(new GetWorkspaceMembershipQuery()
        {
            WorkspaceId = workspaceId,
            UserId = userId
        });

        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("{workspaceId}/members")]
    public async Task<ActionResult<List<UserMembershipDto>>> GetWorkspaceMembers(int workspaceId)
    {
        var userId = User.GetUserId();

        var result = await sender.Send(new GetWorkspaceUsersQuery()
        {
            WorkspaceId = workspaceId,
            UserId = userId
        });

        return HandleResult(result);
    }
    
    [HttpPut]
    [Route("{workspaceId}")]
    public async Task<ActionResult> UpdateWorkspace(int workspaceId, [FromBody]UpdateWorkspaceDto request)
    {
        var userId = User.GetUserId();
        
        if (userId == 0) return Unauthorized();
        
        var result = await sender.Send(new UpdateWorkspaceCommand()
        {
            UserId = userId,
            WorkspaceId = workspaceId,
            WorkspaceName = request.WorkspaceName,
            WorkspaceIsPublic = request.WorkspaceIsPublic
        });

        return HandleResult(result);
    }
    
    [HttpDelete]
    [Route("{workspaceId}")]
    public async Task<ActionResult> DeleteWorkspace(int workspaceId)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new DeleteWorkspaceCommand()
        {
            UserId = userId,
            WorkspaceId = workspaceId
        });

        return HandleResult(result);
    }
    
    [HttpPost]
    [Route("{workspaceId}/members")]
    public async Task<ActionResult> AddtoWorkspace([FromBody]AddToWorkspaceDto request)
    {
        var userId = User.GetUserId();

        if (userId == 0) return Unauthorized();

        var result = await sender.Send(new AddToWorkspaceCommand()
        {
            UserId = userId,
            WorkspaceId = request.WorkspaceId,
            ToAddUserId = request.ToAddUserId,
            ToAddRoleId = request.ToAddRoleId
        });

        return HandleResult(result);
    }
    
    [HttpDelete]
    [Route("{workspaceId}/members")]
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

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<int>> CreateWorkspace([FromBody] CreateWorkspaceDto request)
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
}