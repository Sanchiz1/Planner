using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Tasks.Commands;

public class DeleteTaskCommand : IRequest<Result<bool>>
{
    public int Id { get; init; }
    public int UserId { get; init; }
}

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks.FindAsync(request.Id);

        if (entity is null) return new Exception("Task not found");

        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == entity.WorkspaceId, cancellationToken);

        if (membership is null)
            return new NotFoundException("Not a workspace member");

        if (Role.IsViewerRole(membership.RoleId))
            return new PermissionDeniedException("Only workspace Owner or Member can delete tasks");

        _context.Tasks.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}