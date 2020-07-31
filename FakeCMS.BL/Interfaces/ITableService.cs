using FakeCms.Shared;
using FakeCMS.BL.Services;
using FakeCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FakeCMS.BL.Models.Table;

namespace FakeCMS.BL.Interfaces
{
    public interface ITableService
    {
        Task<Table> GetTable<T>();

        Task<TableDto> GetTable(string tableName);

        Task<TableDto> GetTable(int tableId);

        Task<List<ObjectStateDto>> GetTableData(string tableName);

        Task<List<ObjectStateDto>> GetTableData(int tableId);

        Task<StateFromTableDto> AddStateToTable(StateFromTableDto dto);

        Task<List<TableDto>> List();

        Task AddStateTracking(ObjectStateDto dto);

        Task AddStateTracking<TEntity>(int objectId)
            where TEntity : BaseEntity;

        Task<List<StateFromTableDto>> GetTableStates(int tableId);
    }
}
