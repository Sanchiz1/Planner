using Domain.Entities;
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
    public string displayName { get; set; } = string.Empty;
}
