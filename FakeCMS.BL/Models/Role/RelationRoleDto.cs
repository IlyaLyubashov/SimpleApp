using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.BL.Models.Role
{
    public class RelationRoleDto
    {
        public int ParentRoleId { get; set; }

        public int ChildRoleId { get; set; }
    }
}
