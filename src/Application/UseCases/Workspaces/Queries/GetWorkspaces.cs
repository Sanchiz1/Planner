using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Application.UseCases.Workspaces.Queries;

public record GetWorkspacesQuery : IRequest<Result<List<MembershipDto>>>
{
    public required int UserId { get; init; }
}
public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, Result<List<MembershipDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspacesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<MembershipDto>>> Handle(GetWorkspacesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Memberships
            .AsNoTracking()
            .Where(m => m.UserId == request.UserId)
            .Include(m => m.Workspace)
            .Include(m => m.Role)
            .OrderBy(m => m.Workspace.Name)
            .ProjectTo<MembershipDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}