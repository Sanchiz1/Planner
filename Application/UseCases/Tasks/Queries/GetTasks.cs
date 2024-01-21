using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tasks.Queries;

public record GetTasksQuery : IRequest<Result<List<TaskDto>>>;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, Result<List<TaskDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTasksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<TaskDto>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tasks
                .AsNoTracking()
                .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);
    }
}
