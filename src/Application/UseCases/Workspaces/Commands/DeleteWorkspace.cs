using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record DeleteWorkspaceCommand : IRequest<Result<string>>
{
    public int UserId { get; init; }
    public int WorkspaceId { get; init; }
}

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;

    public DeleteWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var membership = await _context.Memberships
            .Include(m => m.Workspace)
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (membership is null)
            return new NotFoundException("Not a workspace member");

        if (!Role.IsOwnerRole(membership.RoleId))
            return new PermissionDeniedException("Only Owner can delete workspace");

        _context.Workspaces.Remove(membership.Workspace);

        await _context.SaveChangesAsync(cancellationToken);

        return "Workspace deleted successfully";
    }
}