using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record RemoveFromWorkspaceCommand : IRequest<Result<string>>
{
    public int UserId { get; init; }
    public int MembershipId { get; init; }
    public int ToRemoveMembershipId { get; init; }
}

public class RemoveFromWorkspaceCommandHandler : IRequestHandler<RemoveFromWorkspaceCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;

    public RemoveFromWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Handle(RemoveFromWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == request.MembershipId, cancellationToken);
        
        if (membership is null) return new Exception("Membership not found");
        
        if((request.MembershipId != request.ToRemoveMembershipId && !Role.IsOwnerRole(membership.RoleId)) 
           || !membership.IsMembershipOwner(request.UserId)) return new Exception("Permission denied");

        var toRemoveMembership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == request.ToRemoveMembershipId, cancellationToken);
        
        if (toRemoveMembership is null) return new Exception("Membership not found");
        
        if (!Role.IsOwnerRole(toRemoveMembership.RoleId)) return new Exception("Cannot remove owner");
        
        _context.Memberships.Remove(toRemoveMembership);

        await _context.SaveChangesAsync(cancellationToken);

        return "Removed from workspace successfully";
    }
}