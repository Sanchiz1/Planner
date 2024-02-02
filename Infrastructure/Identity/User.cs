using Domain.Membership;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity;

public class User : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public required string displayName { get; set; }
    public List<Membership> Memberships { get; set; } = new List<Membership>();
}
