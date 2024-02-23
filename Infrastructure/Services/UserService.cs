using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<IApplicationUser>> GetWorkspaceUsers(int workSpaceId)
    {
        return await _userManager.Users.Where(u => u.Memberships.Any(m => m.WorkspaceId == workSpaceId)).ToListAsync<IApplicationUser>();
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
