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

        public async Task<int> Create(StateDto dto)
        {
            var state = StateDto.Map(dto);
            await _dbContext.AddAsync(state);
            await _dbContext.SaveChangesAsync();
            return state.Id;
        }
    }
  
}
