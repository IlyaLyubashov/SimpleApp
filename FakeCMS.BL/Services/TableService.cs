using FakeCms.Shared;
using FakeCMS.BL.Interfaces;
using FakeCMS.BL.Models.State;
using FakeCMS.BL.Models.Table;
using FakeCMS.DAL;
using FakeCMS.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FakeCMS.BL.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository _repository;
        private readonly DbContextFakeCms _dbContext;


        public TableService(IRepository repository, DbContextFakeCms dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<Table> GetTable<T>()
        {
            var type = typeof(T);
            var stateAttribute = (type.GetCustomAttributes(typeof(StatefullEntityAttribute), inherit: false)
                as StatefullEntityAttribute[]).Single();
            var table = await _dbContext.Tables.Where(st => st.Name == stateAttribute.TableName).SingleAsync();
            return table;
        }

        public async Task<TableDto> GetTable(string tableName)
        {
            var table = await _dbContext.Tables.Where(st => st.Name == tableName)
                .SingleOrDefaultAsync();
            return TableDto.Map(table);
        }

        public async Task<TableDto> GetTable(int tableId)
        {
            var table = await _dbContext.Tables.Where(st => st.Id == tableId)
                .SingleOrDefaultAsync();
            return TableDto.Map(table);
        }

        public async Task<List<ObjectStateDto>> GetTableData(string tableName)
        {
            var tableData = await _dbContext.ObjectStates.Where(os => os.Table.Name == tableName)
                .ToListAsync();

            return tableData.Select(objState => ObjectStateDto.Map(objState))
                 .ToList();
        }

        public async Task<List<ObjectStateDto>> GetTableData(int tableId)
        {
            var tableData = await _dbContext.ObjectStates.Where(os => os.Table.Id == tableId)
                .ToListAsync();

            return tableData.Select(objState => ObjectStateDto.Map(objState))
                 .ToList();
        }

        public async Task AddStateTracking(ObjectStateDto dto)
        {
            var objectState = ObjectStateDto.Map(dto);
            await _dbContext.AddAsync(objectState);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TableDto>> List()
        {
            var tables = await _dbContext.Tables.ToListAsync();
            return tables.Select(t => TableDto.Map(t))
                .ToList();
        }


        public async Task<List<StateFromTableDto>> GetTableStates(int tableId)
        {
            var stateTableLinks = await _dbContext.StateTables.Where(st => st.TableId == tableId)
                .Include(st => st.State)
                .ToListAsync();

            return stateTableLinks.Select(link => StateFromTableDto.Map(link))
                .ToList();
        }

        public async Task AddStateTracking<TEntity>(int objectId)
            where TEntity : BaseEntity
        {
            var table = await GetTable<TEntity>();

            var entity = await _dbContext.Set<TEntity>().SingleAsync(entity => entity.Id == objectId);
            var statefullManager = new StatefullManager(entity);

            var objectStateDto = new ObjectStateDto
            {
                ObjectId = objectId,
                TableId = table.Id,
                Title = statefullManager.GetTitle(),
                Description = statefullManager.GetDescription()
            };
            await AddStateTracking(objectStateDto);
        }

        public async Task UpdateDataObject(ObjectStateDto objectDto)
        {
            var objectState = await _dbContext.ObjectStates.Where(os => os.ObjectId == objectDto.ObjectId).SingleAsync();
            objectState.StateId = objectDto.StateId;
            objectState.Title = objectDto.Title;
            objectState.Description = objectDto.Description;
            await _dbContext.SaveChangesAsync();
        }
    }
    
}
