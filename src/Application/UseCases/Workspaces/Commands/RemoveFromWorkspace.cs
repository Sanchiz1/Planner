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
    public int MembershipId { get; init; }
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
            .FirstOrDefaultAsync(m => m.Id == request.MembershipId, cancellationToken);
        
        if (membership is null)
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        if ((request.MembershipId != request.ToRemoveMembershipId && !Role.IsOwnerRole(membership.RoleId)) 
           || !membership.IsMembershipOwner(request.UserId)) return new Error(ErrorCodes.PermissionDenied, "Permission denied");

        var toRemoveMembership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == request.ToRemoveMembershipId, cancellationToken);
        
        if (toRemoveMembership is null) return new Error(ErrorCodes.MembershipNotFound, "Membership not found");
        
        if (!Role.IsOwnerRole(toRemoveMembership.RoleId)) return new Error(ErrorCodes.CannotRemoveOwner, "Cannot remove owner");
        
        _context.Memberships.Remove(toRemoveMembership);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}