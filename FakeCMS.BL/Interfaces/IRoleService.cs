using FakeCMS.BL.Models.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Interfaces
{
    public interface IRoleService
    {
        Task<int> Create(CreateRoleDto createRoleDto);

        Task<List<RoleDto>> List();

        Task<List<RoleDto>> UserRoles(int userId);

        //Task RemoveChildRole(RelationRoleDto relationDto);

        //Task<int> AddChildRole(RelationRoleDto relationDto);
    }
}
