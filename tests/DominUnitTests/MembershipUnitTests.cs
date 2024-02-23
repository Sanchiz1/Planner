using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominUnitTests
{
    internal class MembershipUnitTests
    {
        [Test()]
        public void CreateWorkspace(int userId, string workspaceName)
        {
            Membership.CreateWorkspace(userId, workspaceName);
        }
    }
}
