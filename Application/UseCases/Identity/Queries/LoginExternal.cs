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

public record LoginExternalQuery : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}

public class LoginExternalQueryHandler : IRequestHandler<LoginExternalQuery, Result<string>>
{
    private readonly IIdentityService _identityService;

    public LoginExternalQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<string>> Handle(LoginExternalQuery request, CancellationToken cancellationToken)
        => await _identityService.LoginExternalAsync(request.Username, request.Email);
}