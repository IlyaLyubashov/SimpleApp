using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models;
using FakeCMS.BL.Models.Item;
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
    public class ItemService : IItemService
    {
        private readonly IRepository _repository;
        private readonly DbContextFakeCms _dbContext;


        public ItemService(IRepository repository, DbContextFakeCms dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
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
            if (item != null)
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
            var itemDtoList = EnumerableEntityToDto(itemList);
            return itemDtoList;
        }


        public async Task<List<ItemDto>> SliceFromOrderedById(int positionFrom, int positionTo)
        {
            var itemList = await _dbContext.Items.OrderBy(i => i.Id)
                .Skip(positionFrom - 1)
                .Take(positionTo - positionFrom + 1)
                .ToListAsync();
            var itemDtoList = EnumerableEntityToDto(itemList);
            return itemDtoList;
        }


        public async Task<long> Count()
        {
            return await _dbContext.Items.CountAsync();
        }


        public async Task Update(ItemDto itemDto)
        {
            var item = ItemDtoToEntity(itemDto);
            await _repository.Update(item);
        }


        private List<ItemDto> EnumerableEntityToDto(IEnumerable<Item> items)
        { 
            return items.Select(item => ItemDto.FromEntity(item)).ToList();
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
