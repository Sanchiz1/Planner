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
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;

    public IdentityService(
        UserManager<User> userManager,
        SignInManager<User> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> RegisterAsync(string userName, string email, string password)
    {
        var user = new User
        {
            UserName = userName,
            Email = email,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return new Exception(result.Errors.First().Description);
        }

        return "Registered successfully";
    }
    public async Task<Result<string>> LoginAsync(string userName, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(userName,
              password, true, lockoutOnFailure: false);

        var user = await _userManager.FindByNameAsync(userName);

        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var token = _tokenService.GenerateToken(user);

            return token;
        }

        return new Exception("Wrong password or username");
    }
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<string> CreateUserAsync(string userName, string password)
    {
        var user = new User
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        return user.Id;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string Email, string Password)
    {
        var result = await _signInManager.PasswordSignInAsync(Email,
              Password, true, lockoutOnFailure: false);


        return result.Succeeded;
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null) return;
        await DeleteUserAsync(user);
    }

    public async Task DeleteUserAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);
    }
}
