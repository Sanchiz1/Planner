using Application.Common.DTOs;
using Application.Common.Exceptions;
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

namespace Application.UseCases.Users.Queries;

public record GetWorkspaceUsersQuery : IRequest<Result<List<UserMembershipDto>>>
{
    public required int WorkspaceId { get; init; }
    public required int UserId { get; init; }
}
public class GetWorkspaceUsersQueryHandler : IRequestHandler<GetWorkspaceUsersQuery, Result<List<UserMembershipDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetWorkspaceUsersQueryHandler(IApplicationDbContext context, IUserService userService, IMapper mapper)
    {
        _context = context;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<List<UserMembershipDto>>> Handle(GetWorkspaceUsersQuery request, CancellationToken cancellationToken)
    {
        var workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == request.WorkspaceId);

        if (workspace is null) 
            return new NotFoundException("Workspace not found");

        if (!workspace.IsPublic &&
            !await _context.Memberships.AnyAsync(m => m.UserId == request.UserId && m.WorkspaceId == workspace.Id))
                return new PermissionDeniedException("Workspace is private");

        return _mapper.Map<List<UserMembershipDto>>(await _userService.GetWorkspaceUsers(request.WorkspaceId));
    }
}