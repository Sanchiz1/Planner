using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record DeleteWorkspaceCommand : IRequest<Result<string>>
{
    public int UserId { get; init; }
    public int MembershipId { get; init; }
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
        var entity = await _context.Memberships
            .Include(membership => membership.Workspace)
            .FirstOrDefaultAsync(membership => membership.Id == request.MembershipId, cancellationToken);
        
        if (entity == null) return new Exception("Membership not found");
        
        if (entity.RoleId != Role.OwnerRole.Id || entity.UserId != request.UserId) return new Exception("Permission denied");
        
        _context.Workspaces.Remove(entity.Workspace);

        await _context.SaveChangesAsync(cancellationToken);

        return "Workspace deleted successfully";
    }
}