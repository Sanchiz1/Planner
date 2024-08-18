using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Application.UseCases.Roles.Queries;

public record GetRolesQuery : IRequest<Result<List<RoleDto>>>
{

}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<List<RoleDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _context.MembershipRoles
                .AsNoTracking()
                .OrderBy(r => r.Name)
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}
