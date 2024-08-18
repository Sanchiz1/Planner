using Application.Common.DTOs;
using Application.Common.Errors;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Workspaces.Queries;
public record GetWorkspaceMembershipQuery : IRequest<Result<MembershipDto>>
{
    public required int UserId { get; init; }
    public required int WorkspaceId { get; init; }
}
public class GetWorkspaceMembershipQueryHandler : IRequestHandler<GetWorkspaceMembershipQuery, Result<MembershipDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspaceMembershipQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<MembershipDto>> Handle(GetWorkspaceMembershipQuery request, CancellationToken cancellationToken)
    {
        var workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == request.WorkspaceId);

        if (workspace is null)
            return new Error(ErrorCodes.WorkspaceNotFound, "Workspace not found");

        var membership = await _context.Memberships
            .Include(m => m.Role)
            .FirstOrDefaultAsync(m => m.UserId == request.UserId && m.WorkspaceId == workspace.Id);

        if (membership is null)
            return new Error(ErrorCodes.MembershipNotFound, "Not a workspace member");

        return _mapper.Map<MembershipDto>(membership);
    }
}