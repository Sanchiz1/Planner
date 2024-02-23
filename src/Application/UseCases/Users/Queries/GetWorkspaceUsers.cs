using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Queries;

public record GetWorkspaceUsersQuery : IRequest<Result<List<UserDto>>>
{
    public required int WorkspaceId { get; init; }
}
public class GetWorkspaceUsersQueryHandler : IRequestHandler<GetWorkspaceUsersQuery, Result<List<UserDto>>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetWorkspaceUsersQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<List<UserDto>>> Handle(GetWorkspaceUsersQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<UserDto>>(await _userService.GetWorkspaceUsers(request.WorkspaceId));
    }
}