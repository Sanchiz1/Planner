using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IUserService
{
    Task<List<IApplicationUser>> GetWorkspaceUsers(int workSpaceId);
    Task<IApplicationUser> GetUserByEmail(string email);
    Task<IApplicationUser> GetUserById(int id);
}
