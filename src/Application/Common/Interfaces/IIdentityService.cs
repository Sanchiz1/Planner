using Application.Common.Models;
using Shared.Result;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> RegisterAsync(string userName, string email, string password);
    Task<Result<Token>> LoginAsync(string email);
    Task<Result<Token>> LoginPasswordAsync(string email, string password);
    Task<Result<Token>> LoginExternalAsync(string userName, string email);
    Task<bool> IsInRoleAsync(int userId, string role);
    System.Threading.Tasks.Task DeleteUserAsync(int userId);
}
