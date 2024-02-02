using Application.Common.Interfaces;
using Application.UseCases.Identity.Commands;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Identity.Queries;

public record LoginPasswordQuery : IRequest<Result<string>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginPasswordQueryHandler : IRequestHandler<LoginPasswordQuery, Result<string>>
{
    private readonly IIdentityService _identityService;

    public LoginPasswordQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<string>> Handle(LoginPasswordQuery request, CancellationToken cancellationToken)
        => await _identityService.LoginPasswordAsync(request.Email, request.Password);
}