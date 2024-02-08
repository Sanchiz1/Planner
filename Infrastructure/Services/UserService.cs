using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.ViewModels;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<List<UserViewModel>> GetWorkspaceUsers(int workspaceId)
    {
        return await _context.Memberships
            .Where(m => m.WorkspaceId == workspaceId)
            .Select(m =>
                new UserViewModel()
                {
                    User = _userManager.Users
                        .Where(u => u.Id == m.UserId)
                        .Select(u =>
                            new UserDto()
                            {
                                UserId = u.Id,
                                Email = u.Email,
                                DisplayName = u.DisplayName
                            }).First(),
                    Membership = new MembershipDto()
                    {
                        UserId = m.UserId,
                        WorkspaceId = m.WorkspaceId,
                        RoleId = m.RoleId
                    }
                }).ToListAsync();
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return null;

        return new UserDto()
        {
            UserId = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName
        };
    }
}