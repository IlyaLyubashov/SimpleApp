using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models;
using FakeCMS.BL.Models.Item;
using FakeCMS.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace FakeCMS.Controllers.Api
{   
    [Authorize]
    public class ItemController : BaseApiController
    {

        private readonly IItemService _itemService;
        private readonly ITableService _tableService;

        public ItemController(IItemService itemService, ITableService tableService)
        {
            _itemService = itemService;
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = await _itemService.List();
            return Ok(items);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _itemService.GetById(id);
            return Ok(item);
        }

        [HttpGet("listById")]
        public async Task<IActionResult> List(int positionFrom, int positionTo)
        {
            var items = await _itemService.SliceFromOrderedById(positionFrom, positionTo);
            return Ok(items);
        }


        [HttpGet("count")]
        public async Task<long> Count()
        {
            var count = await _itemService.Count();
            return count;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateItemDto createItemDto)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var itemId = await _itemService.Create(createItemDto);
                await _tableService.AddStateTracking<Item>(itemId);
                ts.Complete();
            }
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ItemDto itemDto)
        {
            await _itemService.Update(itemDto);
            return Ok();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteById(id);
            return Ok();
        }

    }
}
