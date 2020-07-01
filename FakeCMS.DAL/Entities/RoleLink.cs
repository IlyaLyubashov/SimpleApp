using FakeCms.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.DAL.Entities
{
    public class RoleLink : BaseEntity
    {
        public int ChildRoleId { get; set; }

        public int ParentRoleId { get; set; }

        public Role ParentRole { get; set; }

        public Role ChildRole { get; set; }
    }
}
