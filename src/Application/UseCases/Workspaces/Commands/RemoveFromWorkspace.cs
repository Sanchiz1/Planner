using Application.Common.Errors;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Application.UseCases.Workspaces.Commands;

public record RemoveFromWorkspaceCommand : IRequest<Result>
{
    public int UserId { get; init; }
    public int WorkspaceId { get; init; }
    public int ToRemoveMembershipId { get; init; }
}

public class RemoveFromWorkspaceCommandHandler : IRequestHandler<RemoveFromWorkspaceCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RemoveFromWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RemoveFromWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var membership = await _context.Memberships
            .Include(m => m.Workspace)
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (membership is null)
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        if (!Role.IsOwnerRole(membership.RoleId))
            return new Error(ErrorCodes.PermissionDenied, "Only Owner can remove members from workspace");

        var toRemoveMembership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == request.ToRemoveMembershipId, cancellationToken);
        
        if (toRemoveMembership is null) return new Error(ErrorCodes.MembershipNotFound, "Membership not found");
        
        if (Role.IsOwnerRole(toRemoveMembership.RoleId)) return new Error(ErrorCodes.CannotRemoveOwner, "Cannot remove Owner");
        
        _context.Memberships.Remove(toRemoveMembership);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}