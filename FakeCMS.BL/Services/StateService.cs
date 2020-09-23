using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.State;
using FakeCMS.BL.Models.Table;
using FakeCMS.DAL;
using FakeCMS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FakeCMS.BL.Services
{
    public class StateService : IStateService
    {
        private readonly IRepository _repository;
        private readonly DbContextFakeCms _dbContext;
        private readonly ITableService _tableService;


        public StateService(IRepository repository, DbContextFakeCms dbContext, ITableService tableService)
        {
            _repository = repository;
            _dbContext = dbContext;
            _tableService = tableService;
        }

        public async Task AddRoleToState(RoleStateDto dto)
        {
            var roleState = RoleStateDto.Map(dto);
            await _dbContext.RoleStates.AddAsync(roleState);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRoleFromState(RoleStateDto dto)
        {
            var roleState = await _dbContext.RoleStates.Where(rs => rs.RoleId == dto.RoleId && rs.StateId == dto.StateId)
                .SingleAsync();
            _dbContext.Remove(roleState);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(StateFromTableDto dto)
        {

        }

        public async Task<StateFromTableDto> AddStateToTable(StateFromTableDto dto)
        {
            var (stateTable, state) = StateFromTableDto.Map(dto);
            state.Id = 0;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbContext.AddAsync(state);
                await _dbContext.SaveChangesAsync();
                stateTable.StateId = state.Id;
                await _dbContext.AddAsync(stateTable);
                await _dbContext.SaveChangesAsync();

                ts.Complete();
            }
            return StateFromTableDto.Map(stateTable);
        }

        public async Task Delete(int stateId)
        {
            var state = await _dbContext.States.Where(s => s.Id == stateId).SingleAsync();
            await _dbContext.StateTables.Where(st => st.StateId == stateId).LoadAsync();
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _dbContext.RemoveRange(state.StateTables);
                _dbContext.Remove(state);
                await ReplaceObjectStates(stateId, null);
                await _dbContext.SaveChangesAsync();
                ts.Complete();
            }
        }

        public async Task ReplaceObjectStates(int? prevStateId, int? newStateid)
        {
            var objectStates = await _dbContext.ObjectStates.Where(os => os.StateId == prevStateId).ToListAsync();
            objectStates.ForEach(os => os.StateId = newStateid);
            await _dbContext.SaveChangesAsync();
        }
    }

}
