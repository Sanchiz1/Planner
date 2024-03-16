using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IUserService
{
    Task<List<UserMembership>> GetWorkspaceUsers(int workSpaceId);
    Task<IApplicationUser> GetUserByEmail(string email);
    Task<IApplicationUser> GetUserById(int id);
}
