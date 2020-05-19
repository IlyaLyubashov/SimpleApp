using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models;
using FakeCMS.BL.Models.Item;
using FakeCMS.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeCMS.Controllers.Api
{
    public class ItemController : BaseApiController
    {
        
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
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


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateItemDto createItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _itemService.Create(createItemDto);
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
