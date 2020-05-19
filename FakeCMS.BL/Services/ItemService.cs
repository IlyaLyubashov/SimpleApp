using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models;
using FakeCMS.BL.Models.Item;
using FakeCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository _repository;

        public ItemService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> Create(CreateItemDto createItemDto)
        {
            var item = CreateItemDtoToEntity(createItemDto);
            var createdItemId = await _repository.Add(item);
            return createdItemId;
        }

        public async Task DeleteById(int id)
        {
            var item = await _repository.GetById<Item>(id);
            await _repository.Delete<Item>(item);
        }

        public async Task<ItemDto> GetById(int id)
        {
            var item = await _repository.GetById<Item>(id);
            return ItemDto.FromEntity(item);
        }

        public async Task<List<ItemDto>> List()
        {
            var itemList = await _repository.List<Item>();
            var itemDtoList = itemList.Select(item => ItemDto.FromEntity(item)).ToList();
            return itemDtoList;
        }

        public async Task Update(ItemDto itemDto)
        {
            var item = ItemDtoToEntity(itemDto);
            await _repository.Update(item);
        }

        private Item CreateItemDtoToEntity(CreateItemDto itemDto)
        {
            return new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Value = itemDto.Value.Value,
                Count = itemDto.Count.Value
            };
        }

        private Item ItemDtoToEntity(ItemDto itemDto)
        {
            return new Item
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                Description = itemDto.Description,
                Count = itemDto.Count,
                Value = itemDto.Value
            };
        }
    }
}
