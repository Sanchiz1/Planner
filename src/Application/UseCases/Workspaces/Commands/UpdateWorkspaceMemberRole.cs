using Application.Common.Errors;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Workspaces.Commands;
public record UpdateWorkspaceMemberRoleCommand : IRequest<Result>
{
    public int UserId { get; init; }
    public int WorkspaceId { get; init; }
    public int ToUpdateMembershipId { get; init; }
    public int ToUpdateRoleId { get; init; }
}

public class UpdateWorkspaceMemberRoleCommandHandler : IRequestHandler<UpdateWorkspaceMemberRoleCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateWorkspaceMemberRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateWorkspaceMemberRoleCommand request, CancellationToken cancellationToken)
    {
        var membership = await _context.Memberships
            .Include(m => m.Workspace)
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (membership is null)
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        if (!Role.IsOwnerRole(membership.RoleId))
            return new Error(ErrorCodes.PermissionDenied, "Only Owner can update members");

        var toUpdateMembership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == request.ToUpdateMembershipId, cancellationToken);

        if (toUpdateMembership is null) return new Error(ErrorCodes.MembershipNotFound, "Membership not found");

        if (Role.IsOwnerRole(request.ToUpdateRoleId)) return new Error(ErrorCodes.OwnerAlreadyExists, "Cannot add another Owner");

        if (Role.IsOwnerRole(toUpdateMembership.RoleId)) return new Error(ErrorCodes.CannotUpdateOwner, "Cannot update Owner");

        toUpdateMembership.UpdateRole(request.ToUpdateRoleId);

        _context.Memberships.Update(toUpdateMembership);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}