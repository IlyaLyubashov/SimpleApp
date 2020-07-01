using FakeCMS.BL.Models.Item;
using FakeCMS.BL.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(int id);

        Task<List<UserDto>> List();

        //Task Update(ItemDto itemDto);

        Task DeleteById(int id);

        Task<List<UserDto>> SliceFromOrderedById(int positionFrom, int positionTo);

        Task<long> Count();

        Task UpdateUserRoles(UpdateUserRolesDto updateUserRolesDto);
    }
}
