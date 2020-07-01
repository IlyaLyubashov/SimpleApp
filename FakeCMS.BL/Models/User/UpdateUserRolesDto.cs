using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.BL.Models.User
{
    public class UpdateUserRolesDto
    {
        public int UserId { get; set; }

        public int[] RoleIds { get; set; }
    }
}
