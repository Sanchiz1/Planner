using Application.Common.Interfaces;
using MediatR;
using Shared;

namespace Application.UseCases.Tasks.Commands;

public class UpdateTaskCommand : IRequest<Result<int>>
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public UpdateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks.FindAsync(request.Id);

        if (entity == null) return new Exception("Task not found");

        entity.UpdateTask(request.Title,
            request.Description,
            request.StartDate,
            request.EndDate);

        _context.Tasks.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}