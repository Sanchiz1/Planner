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

public record GetUserByEmailQuery : IRequest<Result<UserDto>>
{
    public required string Email { get; init; }
}
public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserDto>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<UserDto>(await _userService.GetUserByEmail(request.Email));
    }
}
