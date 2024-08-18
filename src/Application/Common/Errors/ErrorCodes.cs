using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors;
internal static class ErrorCodes
{
    public const string UserNotFound = "USER_NOT_FOUND";

    public const string RoleNotFound = "ROLE_NOT_FOUND";
    
    public const string TaskNotFound = "TASK_NOT_FOUND";
    
    public const string MembershipNotFound = "MEMBERSHIP_NOT_FOUND";
    
    public const string WorkspaceNotFound = "WORKSPACE_NOT_FOUND";
    
    public const string OwnerAlreadyExists = "OWNER_ALREADY_EXISTS";

    public const string CannotRemoveOwner = "CANNOT_REMOVE_OWNER";
    
    public const string PermissionDenied = "PERMISSON_DENIED";
}
