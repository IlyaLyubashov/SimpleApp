using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.UserRole;
using FakeCMS.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly DbContextFakeCms _dbContext;

        public UserRoleService(DbContextFakeCms dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(UserRoleDto createUserRoleDto)
        {
            var userRole = new IdentityUserRole<int>
            {
                UserId = createUserRoleDto.UserId,
                RoleId = createUserRoleDto.RoleId
            };
            await _dbContext.UserRoles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(UserRoleDto userRoleDto)
        {
            var userRoleToDelete = await _dbContext.UserRoles.Where(ur => ur.RoleId == userRoleDto.RoleId &&
                ur.UserId == userRoleDto.UserId)
                .SingleOrDefaultAsync();
            _dbContext.Remove(userRoleToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
