using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.BL.Models.Role
{
    public class UserHasRoleDto : RoleWithChildrenDto
    {
        public UserRoleRelation RoleRelationToUser { get; set; }
    }

    public class RoleWithChildrenDto : RoleDto
    {
        public List<RoleDto> Children { get; set; }
    }

    public enum UserRoleRelation
    { 
        HasNot,
        Has,
        Inherit
    }
}
