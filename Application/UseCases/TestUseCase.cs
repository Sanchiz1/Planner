using Application.Common.Interfaces;
using Application.UseCases.Tasks.Commands;
using Domain.Membership;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases;

public record TestUseCaseCommand : IRequest<Result<int>>
{
    public required string UserId { get; set; }
}
public class TestUseCaseCommandHandler : IRequestHandler<TestUseCaseCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public TestUseCaseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(TestUseCaseCommand request, CancellationToken cancellationToken)
    {
        return 1;
    }
}
