﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Result;

namespace Infrastructure.Services;

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
            return new Error("FAILED_TO_CREATE_USER", result.Errors.First().Description);
        }

        return "Registered successfully";
    }

    public async Task<Result<Token>> LoginAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return new Error("USER_NOT_FOUND", "User not found");

        await _signInManager.SignInAsync(user, true);

        var token = _tokenService.GenerateToken(user);

        return token;
    }
    public async Task<Shared.Result.Result<Token>> LoginPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return new Error("INVALID_PASSWORD_OR_USERNAME", "Invalid password ot username");

        if (!await _userManager.CheckPasswordAsync(user, password)) return new Error("invalid_credentials", "Invalid password ot username");

        await _signInManager.SignInAsync(user, true);

        var token = _tokenService.GenerateToken(user);

        return token;
    }

    public async Task<Result<Token>> LoginExternalAsync(string userName, string email)
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
                return new Error("FAILED_TO_CREATE_USER", result.Errors.First().Description);
            }
        }

        return await LoginAsync(email);
    }

    public async System.Threading.Tasks.Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async System.Threading.Tasks.Task DeleteUserAsync(int userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null) return;
        await DeleteUserAsync(user);
    }

    public async System.Threading.Tasks.Task DeleteUserAsync(ApplicationUser applicationUser)
    {
        var result = await _userManager.DeleteAsync(applicationUser);
    }


    public async Task<List<Membership>> GetUserMemberships(int userId)
    {
        var user = await _userManager.Users.Include(u => u.Memberships).FirstOrDefaultAsync(u => u.Id == userId);

        return user.Memberships;
    }
}
