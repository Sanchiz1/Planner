using Application.Common.DTOs;

namespace Application.Common.ViewModels;

public class UserViewModel
{
    public UserDto User { get; set; }
    public MembershipDto Membership { get; set; }
}