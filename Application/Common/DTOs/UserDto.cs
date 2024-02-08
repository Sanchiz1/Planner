using Domain.Entities;

namespace Application.Common.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
}