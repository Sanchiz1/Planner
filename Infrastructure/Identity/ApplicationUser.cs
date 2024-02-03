using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    [Required]
    [MaxLength(100)]
    public required string DisplayName { get; set; }
}