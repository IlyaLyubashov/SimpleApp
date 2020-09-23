using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.Table;
using FakeCMS.BL.Services;
using FakeCMS.Controllers.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeCMS.Api
{
    public class TableController : BaseApiController
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tables = await _tableService.List();
            return Ok(tables);
        }

        [HttpGet("{tableId:int}")]
        public async Task<IActionResult> GetTable(int tableId)
        {
            var table = await _tableService.GetTable(tableId);
            return Ok(table);
        }

        [HttpGet("{tableId:int}/states")]
        public async Task<IActionResult> GetTableStates(int tableId)
        {
            var states = await _tableService.GetTableStates(tableId);
            return Ok(states);
        }

        [HttpGet("{tableId:int}/data")]
        public async Task<IActionResult> GetData(int tableId)
        {
            var tableData = await _tableService.GetTableData(tableId);
            return Ok(tableData);
        }

    }
}