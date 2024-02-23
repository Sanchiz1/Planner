using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationUser
{
    int Id { get; set; }
    string DisplayName { get; set; }
    string Email { get; set; }
}