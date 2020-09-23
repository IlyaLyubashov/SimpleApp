using FakeCMS.BL.Models.State;
using FakeCMS.BL.Models.Table;
using FakeCMS.BL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Interfaces
{
    public interface IStateService
    {
        Task AddRoleToState(RoleStateDto dto);

        Task RemoveRoleFromState(RoleStateDto dto);

        Task Update(StateFromTableDto dto);

        Task<StateFromTableDto> AddStateToTable(StateFromTableDto dto);

        Task Delete(int stateId);
    }
}
