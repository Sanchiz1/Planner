using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs
{
    public class UserMembershipDto
    {
        public MembershipDto Membership { get; set; }
        public UserDto User { get; set; }
    }
}
