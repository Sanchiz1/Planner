using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tasks.Commands;

public record CreateTaskCommand : IRequest<Result<int>>
{
    public int WorkspaceId { get; init; }
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Task(
            request.WorkspaceId,
            request.Title,
            request.Description,
            request.StartDate,
            request.EndDate
        );

        _context.Tasks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
