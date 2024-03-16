using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Commands;

public record AddToWorkspaceCommand : IRequest<Result<string>>
{
    public int UserId { get; init; }
    public int WorkspaceId { get; init; }
    public int ToAddUserId { get; init; }
    public int ToAddRoleId { get; init; }
}

public class AddToWorkspaceCommandHandler : IRequestHandler<AddToWorkspaceCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserService _userService;

    public AddToWorkspaceCommandHandler(IApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<Result<string>> Handle(AddToWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var membership = await _context.Memberships
            .Include(m => m.Workspace)
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (membership is null)
            return new NotFoundException("Not a workspace member");

        if (!Role.IsOwnerRole(membership.RoleId))
            return new PermissionDeniedException("Only Owner can add members to workspace");

        var user = await _userService.GetUserById(request.ToAddUserId);

        if (user is null)
            return new NotFoundException("User does not exist");

        var role = await _context.MembershipRoles.FirstOrDefaultAsync(r => r.Id == request.ToAddRoleId);

        if (role is null)
            return new NotFoundException("Role does not exist");

        if (Role.IsOwnerRole(role.Id))
            return new Exception("Cannot add another Owner");

        var newMembership = Membership.AddToWorkspace(request.ToAddUserId, membership.Workspace, request.ToAddRoleId);

        await _context.Memberships.AddAsync(newMembership);

        await _context.SaveChangesAsync(cancellationToken);

        return "Added to workspace successfully";
    }
}