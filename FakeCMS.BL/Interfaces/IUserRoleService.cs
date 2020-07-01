using FakeCMS.BL.Models.UserRole;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Interfaces
{
    public interface IUserRoleService
    {
        Task Create(UserRoleDto createUserRoleDto);

        Task Delete(UserRoleDto userRoleDto);
    }
}
