using System;
using System.Collections.Generic;
using System.Text;
using RoleEntity = FakeCMS.DAL.Entities.Role;

namespace FakeCMS.BL.Models.Role
{
    public class RoleDto : CreateRoleDto
    {
        public int Id { get; set; }


        public static RoleDto FromEntity(RoleEntity role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
