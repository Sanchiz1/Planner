using Domain.Entities;
using Shared;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> RegisterAsync(string userName, string email, string password);
    Task<Result<string>> LoginAsync(string email);
    Task<Result<string>> LoginPasswordAsync(string email, string password);
    Task<Result<string>> LoginExternalAsync(string userName, string email);
    Task<bool> IsInRoleAsync(int userId, string role);
    System.Threading.Tasks.Task DeleteUserAsync(int userId);
}
