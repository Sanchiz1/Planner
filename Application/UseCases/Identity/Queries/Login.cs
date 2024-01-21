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

public record LoginQuery : IRequest<Result<string>>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<string>>
{
    private readonly IIdentityService _identityService;

    public LoginQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
        => await _identityService.LoginAsync(request.Username, request.Password);
}