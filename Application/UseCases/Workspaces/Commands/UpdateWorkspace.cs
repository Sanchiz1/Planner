using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record UpdateWorkspaceCommand : IRequest<Result<int>>
{
    public int MembershipId { get; init; }
    public required string WorkspaceName { get; init; }
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
            .FirstOrDefaultAsync(membership => membership.Id == request.MembershipId, cancellationToken);
        
        if (entity == null) return new Exception("Membership not found");
        
        if (entity.Workspace == null) return new Exception("Workspace not found");
        
        if (entity.RoleId != Role.OwnerRole.Id) return new Exception("Permission denied");

        entity.Workspace.UpdateWorkspace(request.WorkspaceName);
        
        _context.Memberships.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}