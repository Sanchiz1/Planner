using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Application.UseCases.Tasks.Commands;

public class UpdateTaskCommand : IRequest<Result>
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int UserId { get; init; }
}

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks.FindAsync(request.Id);

        if (entity is null) return new Error(ErrorCodes.TaskNotFound, "Task not found");

        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == entity.WorkspaceId, cancellationToken);

        if (membership is null)
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        if (Role.IsViewerRole(membership.RoleId))
            return new Error(ErrorCodes.PermissionDenied, "Only workspace Owner or Member can update tasks");

        entity.UpdateTask(request.Title,
            request.Description,
            request.StartDate,
            request.EndDate);

        _context.Tasks.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}