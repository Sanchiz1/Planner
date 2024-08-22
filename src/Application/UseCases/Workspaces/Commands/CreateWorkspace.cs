using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.Result;

namespace Application.UseCases.Workspaces.Commands;

public record CreateWorkspaceCommand : IRequest<Result<int>>
{
    public int UserId { get; init; }
    public required string WorkspaceName { get; init; }
    public required bool WorkspaceIsPublic { get; init; }
}

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var entity = Membership.CreateWorkspace(request.UserId, request.WorkspaceName, request.WorkspaceIsPublic);

        this._context.MembershipRoles.Attach(entity.Role);
        
        await _context.Memberships.AddAsync(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Workspace.Id;
    }
}