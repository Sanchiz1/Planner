using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Result;
using System.Drawing;

namespace Application.UseCases.Users.Queries;

public record GetUsersQuery : IRequest<Result<List<UserDto>>>
{
    public string Email { get; init; } = string.Empty;
    public int Page { get; init; } = 1;
    public int Size { get; init; } = 10;
}
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<List<UserDto>>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userService.GetUsers();

        if (!string.IsNullOrEmpty(request.Email))
        {
            users = users.Where(
                u => u.Email.ToLower().Contains(request.Email.ToLower()));
        }

        users = users
            .Skip(request.Size * (request.Page - 1))
            .Take(request.Size).OrderBy(u => u.Id);

        return _mapper.Map<List<UserDto>>(await users.ToListAsync());
    }
}
