using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.Role;
using FakeCMS.BL.Models.User;
using FakeCMS.DAL;
using FakeCMS.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly DbContextFakeCms _dbContext;


        public UserService(IRepository repository, DbContextFakeCms dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task DeleteById(int id)
        {
            var userToDelete = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (userToDelete != null)
                _dbContext.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id); ;
            return UserDto.FromEntity(user);
        }

        public async Task<List<UserDto>> List()
        {
            var omUsers = await _dbContext.Users.ToListAsync();
            var dtoUsers = EnumerableEntityToDto(omUsers);
            return dtoUsers;
        }


        public async Task<List<UserDto>> SliceFromOrderedById(int positionFrom, int positionTo)
        {
            var omUsers = await _dbContext.Users.OrderBy(i => i.Id)
                .Skip(positionFrom - 1)
                .Take(positionTo - positionFrom + 1)
                .ToListAsync();
            var dtoUsers = EnumerableEntityToDto(omUsers);
            return dtoUsers;
        }


        public async Task<long> Count()
        {
            return await _dbContext.Users.CountAsync();
        }


        private List<UserDto> EnumerableEntityToDto(IEnumerable<User> users)
        {
            return users.Select(user => UserDto.FromEntity(user)).ToList();
        }

        //public async Task<List<UserHasRoleDto>> RelationalRoles(int userId)
        //{
        //    var roleEqualityComparer = new RoleEqualityComparer();
        //    var roles = (await _dbContext.Roles.Include(r => r.LinksToChildren).ToListAsync());
        //    roles.
        //    var userRoles = (await _dbContext.UserRoles.Where(ur => ur.UserId == userId).ToListAsync())
        //        .Select(ur => roleIdToRole[ur.RoleId])
        //        .ToHashSet(,);
        //    var processedRoles = new HashSet<Role>();
        //    var userHasRoles = roles.Select(r => new UserHasRoleDto { Id = r.Id, Name = r.Name});

        //    foreach (var role in userRoles)
        //    {
        //        userHasRoles.Add(new UserHasRoleDto
        //        {
        //            Id = role.Id,
        //            Name = role.Name,
        //            Children = role.LinksToChildren.Se
        //        });
        //    }

        public async Task UpdateUserRoles(UpdateUserRolesDto dto)
        {
            var existingRoleIds = (await _dbContext.UserRoles.Where(ur => ur.UserId == dto.UserId).ToListAsync())
                .Select(ur => ur.RoleId)
                .ToHashSet();

            foreach (var roleId in dto.RoleIds)
            {
                if (existingRoleIds.Contains(roleId))
                    existingRoleIds.Remove(roleId);
                else
                {
                    var newUserRoleLink = new IdentityUserRole<int>
                    {
                        UserId = dto.UserId,
                        RoleId = roleId
                    };
                    await _dbContext.UserRoles.AddAsync(newUserRoleLink);
                }    
            }

            var rolesToUnbind = await _dbContext.UserRoles.Where(ur => existingRoleIds.Contains(ur.RoleId) && ur.UserId == dto.UserId )
                .ToListAsync();
            rolesToUnbind.ForEach(r => _dbContext.UserRoles.Remove(r));

            await _dbContext.SaveChangesAsync();
        }



    }

    public class RoleEqualityComparer : IEqualityComparer<Role>
    {
        public bool Equals(Role x, Role y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Role obj)
        {
            return obj.Id.GetHashCode();
        }
    }

}
