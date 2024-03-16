using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.UseCases.Workspaces.Queries;

public record GetWorkspacesQuery : IRequest<Result<List<WorkspaceDto>>>
{
    public required int UserId { get; init; }
}
public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, Result<List<WorkspaceDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspacesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<WorkspaceDto>>> Handle(GetWorkspacesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Memberships
            .AsNoTracking()
            .Where(m => m.UserId == request.UserId)
            .Select(m => m.Workspace)
            .OrderBy(w => w.Name)
            .ProjectTo<WorkspaceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}