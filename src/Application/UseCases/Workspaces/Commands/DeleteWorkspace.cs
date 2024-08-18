using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Application.UseCases.Workspaces.Commands;

public record DeleteWorkspaceCommand : IRequest<Result>
{
    public int UserId { get; init; }
    public int WorkspaceId { get; init; }
}

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var membership = await _context.Memberships
            .Include(m => m.Workspace)
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (membership is null)
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        if (!Role.IsOwnerRole(membership.RoleId))
            return new Error(ErrorCodes.PermissionDenied, "Only Owner can delete workspace");

        _context.Workspaces.Remove(membership.Workspace);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}