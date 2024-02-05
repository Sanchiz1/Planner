using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record RemoveFromWorkspaceCommand : IRequest<Result<string>>
{
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
            .FirstOrDefaultAsync(membership => membership.Id == request.MembershipId, cancellationToken);
        
        if (membership == null) return new Exception("Membership not found");
        
        if (membership.RoleId != Role.OwnerRole.Id) return new Exception("Permission denied");

        var entity = await _context.Memberships
            .FirstOrDefaultAsync(membership => membership.Id == request.ToRemoveMembershipId, cancellationToken);
        
        if (entity == null) return new Exception("Membership not found");
        
        if (entity.RoleId != Role.OwnerRole.Id) return new Exception("Cannot remove owner");
        
        _context.Memberships.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return "Removed from workspace successfully";
    }
}