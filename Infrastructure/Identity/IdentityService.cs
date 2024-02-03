using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TokenService _tokenService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    public async Task<Result<string>> RegisterAsync(string userName, string email, string password)
    {
        var user = new ApplicationUser
        {
            DisplayName = userName,
            UserName = email,
            Email = email,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return new Exception(result.Errors.First().Description);
        }

        return "Registered successfully";
    }
    public async Task<Result<string>> LoginAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return new Exception("User not found");

        await _signInManager.SignInAsync(user, true);

        var token = _tokenService.GenerateToken(user);

        return token;
    }
    public async Task<Result<string>> LoginPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return new Exception("Invalid password ot username");

        if (!await _userManager.CheckPasswordAsync(user, password)) return new Exception("Invalid password ot username");

        await _signInManager.SignInAsync(user, true);

        var token = _tokenService.GenerateToken(user);

        return token;
    }
    public async Task<Result<string>> LoginExternalAsync(string userName, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            var registrationUser = new ApplicationUser
            {
                DisplayName = userName,
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(registrationUser);

            if (!result.Succeeded)
            {
                return new Exception(result.Errors.First().Description);
            }
        }

        return await LoginAsync(email);
    }
    public async Task<string?> GetUserNameAsync(int userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null) return;
        await DeleteUserAsync(user);
    }

    public async Task DeleteUserAsync(ApplicationUser applicationUser)
    {
        var result = await _userManager.DeleteAsync(applicationUser);
    }
}
