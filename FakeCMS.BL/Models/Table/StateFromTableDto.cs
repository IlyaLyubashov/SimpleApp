using FakeCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using StateEntity = FakeCMS.DAL.Entities.State;

namespace FakeCMS.BL.Models.Table
{
    public class StateFromTableDto : StateDto
    {
        public int Index { get; set; }

        public int TableId { get; set; }

        public static StateFromTableDto Map(StateTable stateTableLink)
        {
            var state = stateTableLink.State;
            return new StateFromTableDto
            {
                Id = state.Id,
                Name = state.Name,

                Index = stateTableLink.IndexInTable,
                TableId = stateTableLink.TableId
            };
        }

        public static (StateTable, StateEntity) Map(StateFromTableDto dto)
        {
            var stateTable = new StateTable
            {
                TableId = dto.TableId,
                IndexInTable = dto.Index,
                StateId = dto.Id
            };

            var state = new StateEntity
            {
                Id = dto.Id,
                Name = dto.Name
            };

            return (stateTable, state);
        }
    }

    public class StateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public static StateEntity Map(StateDto dto)
        {
            return new StateEntity
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static StateDto Map(StateEntity entity)
        {
            return new StateDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
