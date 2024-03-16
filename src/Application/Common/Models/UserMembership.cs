using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class UserMembership
    {
        public Membership Membership { get; set; }
        public IApplicationUser User { get; set; }
    }
}
