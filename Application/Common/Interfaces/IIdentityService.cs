using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> RegisterAsync(string userName, string email, string password);
    Task<Result<string>> LoginAsync(string email);
    Task<Result<string>> LoginPasswordAsync(string email, string password);
    Task<Result<string>> LoginExternalAsync(string userName, string email);
    Task<string?> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task DeleteUserAsync(string userId);
}
