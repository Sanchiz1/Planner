using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Application.UseCases.Tasks.Queries;

public record GetWorkspaceTasksQuery : IRequest<Result<List<TaskDto>>>
{
    public required int WorkspaceId { get; init; }
    public required int UserId { get; init; }
}
public class GetWorkspaceTasksQueryHandler : IRequestHandler<GetWorkspaceTasksQuery, Result<List<TaskDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspaceTasksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<TaskDto>>> Handle(GetWorkspaceTasksQuery request, CancellationToken cancellationToken)
    {
        var workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == request.WorkspaceId);

        if (workspace == null) return new Error(ErrorCodes.WorkspaceNotFound, "Workspace not found");

        if (!workspace.IsPublic)
        {
            var membership = await _context.Memberships.FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == workspace.Id);

            if (membership == null) return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");
        }

        return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.WorkspaceId == workspace.Id)
                .OrderBy(t => t.Title)
                .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}
