using FakeCMS.BL.Models;
using FakeCMS.BL.Models.Item;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Interfaces
{
    public interface IItemService
    {
        Task<ItemDto> GetById(int id);

        Task<List<ItemDto>> List();

        Task<long> Create(CreateItemDto itemDto);

        Task Update(ItemDto itemDto);

        Task DeleteById(int id);
        Task<List<ItemDto>> SliceFromOrderedById(int positionFrom, int positionTo);

        Task<long> Count();
    }
}
