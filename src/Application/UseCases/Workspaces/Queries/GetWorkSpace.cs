using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Workspaces.Queries;

public record GetWorkspaceQuery : IRequest<Result<WorkspaceDto>>
{
    public required int UserId { get; init; }
    public required int WorkspaceId { get; init; }
}
public class GetWorkspaceQueryHandler : IRequestHandler<GetWorkspaceQuery, Result<WorkspaceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspaceQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<WorkspaceDto>> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
    {
        var workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == request.WorkspaceId);

        if (workspace is null)
            return new Error(ErrorCodes.WorkspaceNotFound, "Workspace not found");

        if (!workspace.IsPublic &&
            !await _context.Memberships.AnyAsync(m => m.UserId == request.UserId && m.WorkspaceId == workspace.Id))
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        return _mapper.Map<WorkspaceDto>(workspace);
    }
}