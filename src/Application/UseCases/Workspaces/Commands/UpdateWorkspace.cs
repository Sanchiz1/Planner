using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record UpdateWorkspaceCommand : IRequest<Result<int>>
{
    public int UserId { get; init; }
    public int WorkspaceId { get; init; }
    public required string WorkspaceName { get; init; }
    public required bool WorkspaceIsPublic { get; init; }
}

public class UpdateWorkspaceCommandHandler : IRequestHandler<UpdateWorkspaceCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Memberships
            .Include(membership => membership.Workspace)
            .FirstOrDefaultAsync(membership => membership.UserId == request.UserId && membership.WorkspaceId == request.WorkspaceId, cancellationToken);
        
        if (entity is null)
            return new NotFoundException("Not a member of the workspace");
        
        if (!Role.IsOwnerRole(entity.RoleId)) return new PermissionDeniedException("Only Owner can edit workspace");

        entity.Workspace.UpdateWorkspace(request.WorkspaceName, request.WorkspaceIsPublic);
        
        _context.Memberships.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}