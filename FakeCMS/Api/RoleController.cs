using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.Role;
using FakeCMS.Controllers.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeCMS.Api
{
    [Authorize]
    public class RoleController : BaseApiController
    {
        private readonly IRoleService _roleService;


        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var dtoRoles = await _roleService.List();
            return Ok(dtoRoles);
        }


        [HttpPut]
        public async Task<IActionResult> Create(CreateRoleDto createRoleDto)
        {
            await _roleService.Create(createRoleDto);
            return Ok();
        }

    }
}
