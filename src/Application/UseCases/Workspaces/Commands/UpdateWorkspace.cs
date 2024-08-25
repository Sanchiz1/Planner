using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Application.UseCases.Workspaces.Commands;

public record UpdateWorkspaceCommand : IRequest<Result>
{
    public int UserId { get; init; }
    public int WorkspaceId { get; init; }
    public required string WorkspaceName { get; init; }
    public required bool WorkspaceIsPublic { get; init; }
}

public class UpdateWorkspaceCommandHandler : IRequestHandler<UpdateWorkspaceCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Memberships
            .Include(membership => membership.Workspace)
            .FirstOrDefaultAsync(membership => membership.UserId == request.UserId && membership.WorkspaceId == request.WorkspaceId, cancellationToken);
        
        if (entity is null)
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        if (!Role.IsOwnerRole(entity.RoleId)) return new Error(ErrorCodes.PermissionDenied, "Only Owner can edit workspace");

        entity.Workspace.UpdateWorkspace(request.WorkspaceName, request.WorkspaceIsPublic);
        
        _context.Memberships.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}