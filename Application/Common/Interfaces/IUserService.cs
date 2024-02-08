using Application.Common.DTOs;
using Application.Common.ViewModels;

namespace Application.Common.Interfaces;

public interface IUserService
{
    Task<List<UserViewModel>> GetWorkspaceUsers(int workspaceId);
    Task<UserDto> GetUserByEmail(string email);
}