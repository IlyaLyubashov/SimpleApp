using FakeCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeCMS.BL.Models.Table
{
    public class StateTableLinkDto
    {
        public int StateId { get; set; }

        public int TableId { get; set; }

        public static StateTable Map(StateTableLinkDto dto)
        {
            return new StateTable
            {
                StateId = dto.StateId,
                TableId = dto.TableId
            };
        }
    }
}
