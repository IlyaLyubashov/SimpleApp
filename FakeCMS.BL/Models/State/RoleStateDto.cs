using FakeCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.BL.Models.State
{
    public class RoleStateDto
    {
        public int RoleId { get; set; }

        public int StateId { get; set; }

        public static RoleStateDto Map(RoleState roleState)
        {
            return new RoleStateDto
            {
                RoleId = roleState.RoleId,
                StateId = roleState.StateId
            };
        }

        public static RoleState Map(RoleStateDto dto)
        {
            return new RoleState
            {
                RoleId = dto.RoleId,
                StateId = dto.StateId
            };
        }
    }
}
