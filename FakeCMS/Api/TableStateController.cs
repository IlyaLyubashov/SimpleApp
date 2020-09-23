using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.Table;
using FakeCMS.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeCMS.Api
{
    public class TableStateController : BaseApiController
    {
        private readonly IStateService _stateService;

        public TableStateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddState([FromBody]StateFromTableDto dto)
        {
            var stateFromTableDto = await _stateService.AddStateToTable(dto);
            return Ok(stateFromTableDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]StateFromTableDto dto)
        {
            await _stateService.Update(dto);
            return Ok();
        }

        [HttpDelete("{stateId:int}")]
        public async Task<IActionResult> Delete(int stateId)
        {
            await _stateService.Delete(stateId);
            return Ok();
        }

    }
}
