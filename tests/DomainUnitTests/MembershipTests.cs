using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainUnitTests
{
    internal class MembershipTests
    {
        [TestCase(1, "New Workspace", true)]
        public void CreateWorkspace_Returns(int userId, string workspaceName, bool isPublic)
        {
            Membership memberShip = Membership.CreateWorkspace(userId, workspaceName, isPublic);

            Role role = Role.OwnerRole;
            Workspace workspace = new Workspace(workspaceName, isPublic);

            //Owner role
            Assert.That(memberShip.Role, Is.EqualTo(role));

            //workspace
            Assert.That(memberShip.Workspace, Is.EqualTo(workspace));
        }

        [TestCase(1, 2, true)]
        [TestCase(1, 3, true)]
        public void AddToWorkspace_Returns(int userId, int roleId, bool isPublic)
        {
            string workspaceName = "Test workspace";

            Workspace workspace = new Workspace(workspaceName, isPublic);

            Membership memberShip = Membership.AddToWorkspace(userId, workspace, roleId);

            //Role
            Assert.That(memberShip.RoleId, Is.EqualTo(roleId));

            //workspace
            Assert.That(memberShip.Workspace, Is.EqualTo(workspace));
        }

        [TestCase(1, 1, true)]
        public void AddToWorkspace_CannotAddOwner_ThrowsArgumentException(int userId, int roleId, bool isPublic)
        {
            string workspaceName = "Test workspace";

            Workspace workspace = new Workspace(workspaceName, isPublic);

            Assert.Throws<ArgumentException>(() => Membership.AddToWorkspace(userId, workspace, roleId));
        }

        [TestCase(1, 1, true)]
        [TestCase(1, 2, true)]
        public void IsMembershipOwner_Returns(int userId, int ownerId, bool isPublic)
        {
            bool isOwner = userId == ownerId;

            string workspaceName = "Test workspace";

            Membership memberShip = Membership.CreateWorkspace(ownerId, workspaceName, isPublic);

            //IsOwner
            Assert.That(memberShip.IsMembershipOwner(userId), Is.EqualTo(isOwner));
        }
    }
}
