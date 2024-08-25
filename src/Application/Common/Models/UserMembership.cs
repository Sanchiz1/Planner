using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Common.Models
{
    public class UserMembership
    {
        public Membership Membership { get; set; }
        public IApplicationUser User { get; set; }
    }
}
