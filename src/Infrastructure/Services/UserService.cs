using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;

    public UserService(
        UserManager<ApplicationUser> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<List<UserMembership>> GetWorkspaceUsers(int workSpaceId)
    {
        var res = await _context.Memberships.Where(m => m.WorkspaceId == workSpaceId).Join(_userManager.Users, m => m.UserId, u => u.Id, (m, u) =>
            new UserMembership
            {
                User = u,
                Membership = m
            }
        ).ToListAsync();

        return res;
    }

    public async Task<IApplicationUser> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IApplicationUser> GetUserById(int id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
