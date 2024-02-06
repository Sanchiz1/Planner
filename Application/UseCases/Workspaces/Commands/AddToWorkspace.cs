using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record AddToWorkspaceCommand : IRequest<Result<string>>
{
    public int UserId { get; init; }
    public int MembershipId { get; init; }
    public int ToAddUserId { get; init; }
    public int ToAddRoleId { get; init; }
}

public class AddToWorkspaceCommandHandler : IRequestHandler<AddToWorkspaceCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;

    public AddToWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Handle(AddToWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var membership = await _context.Memberships
            .Include(membership => membership.Workspace)
            .FirstOrDefaultAsync(membership => membership.Id == request.MembershipId, cancellationToken);
        
        if (membership == null) return new Exception("Membership not found");
        
        if (membership.RoleId != Role.OwnerRole.Id || membership.UserId != request.UserId) return new Exception("Permission denied");

        var entity = Membership.AddToWorkspace(request.ToAddUserId, membership.Workspace, request.ToAddRoleId);
        
        _context.Memberships.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return "Added to workspace successfully";
    }
}