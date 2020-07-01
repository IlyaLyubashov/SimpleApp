using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.User;
using FakeCMS.BL.Models.UserRole;
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
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;


        public UserController(IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var dtoUsers = await _userService.List();
            return Ok(dtoUsers);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dtoUser = await _userService.GetById(id);
            return Ok(dtoUser);
        }

        [HttpGet("listById")]
        public async Task<IActionResult> List(int positionFrom, int positionTo)
        {
            var dtoUsers = await _userService.SliceFromOrderedById(positionFrom, positionTo);
            return Ok(dtoUsers);
        }

        [HttpGet("count")]
        public async Task<long> Count()
        {
            var count = await _userService.Count();
            return count;
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleToUser([FromBody]UserRoleDto createUserRoleDto)
        {
            await _userRoleService.Create(createUserRoleDto);
            return Ok();
        }

        [HttpPost("removeRole")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody]UserRoleDto removeUserRoleDto)
        {
            await _userRoleService.Delete(removeUserRoleDto);
            return Ok();
        }

        [HttpGet("roles/{userId:int}")]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var dtoRoles = await _roleService.UserRoles(userId);
            return Ok(dtoRoles);
        }

        [HttpPost("roles/update")]
        public async Task<IActionResult> UpdateRoles([FromBody]UpdateUserRolesDto updateUserRolesDto)
        {
            await _userService.UpdateUserRoles(updateUserRolesDto);
            return Ok();
        }
    }
}
