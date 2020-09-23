using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.Table;
using FakeCMS.Controllers.Api;
using FakeCMS.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeCMS.Api
{
    public class TableObjectController : BaseApiController
    {
        private readonly ITableService _tableService;

        public TableObjectController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ObjectStateDto dto)
        {
            await _tableService.UpdateDataObject(dto);
            return Ok();
        }
    }
}
