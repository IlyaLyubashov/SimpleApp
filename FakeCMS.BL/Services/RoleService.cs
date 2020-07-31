using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.Role;
using FakeCMS.DAL;
using FakeCMS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Services
{
    public class RoleService : IRoleService
    {
        private readonly DbContextFakeCms _dbContext;
        private readonly IRepository _repository;


        public RoleService(DbContextFakeCms dbContext, IRepository repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }


        public async Task<int> Create(CreateRoleDto createRoleDto)
        {
            var role = new Role
            {
                Name = createRoleDto.Name
            };
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return role.Id;
        }


        public async Task<List<RoleDto>> List()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            return EnumerableEntityToDto(roles);
        }


        public async Task<List<RoleDto>> UserRoles(int userId)
        {
            var userRoleEntities = await _dbContext.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
            var roleIds = userRoleEntities.Select(ur => ur.RoleId).ToArray();
            var roles = await _dbContext.Roles.Where(r => roleIds.Contains(r.Id))
                .ToListAsync();
            return EnumerableEntityToDto(roles);
        }


        private List<RoleDto> EnumerableEntityToDto(IEnumerable<Role> roles)
        {
            return roles.Select(role => RoleDto.FromEntity(role)).ToList();
        }


        //public async Task<int> AddChildRole(RelationRoleDto relationDto)
        //{
        //    var roleLink = new RoleLink
        //    {
        //        ChildRoleId = relationDto.ChildRoleId,
        //        ParentRoleId = relationDto.ParentRoleId
        //    };

        //    await _dbContext.RoleLinks.AddAsync(roleLink);
        //    await _dbContext.SaveChangesAsync();
        //    return roleLink.Id;
        //}


        //public async Task RemoveChildRole(RelationRoleDto relationDto)
        //{
        //    var roleLink = await _dbContext.RoleLinks.Where(rl => rl.ParentRoleId == relationDto.ParentRoleId &&
        //                                                    rl.ChildRoleId == relationDto.ChildRoleId)
        //        .SingleOrDefaultAsync();
        //    _dbContext.RoleLinks.Remove(roleLink);
        //    await _dbContext.SaveChangesAsync();
        //}
    }
}
