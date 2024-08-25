using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>, IApplicationUser
{
    [Required]
    [MaxLength(100)]
    public required string DisplayName { get; set; }
    public List<Membership> Memberships { get; set; } = new List<Membership>();
}