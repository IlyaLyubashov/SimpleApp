using FakeCms.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.DAL
{
    public class FakeCmsRepository : IRepository
    {
        private readonly DbContextFakeCms _dbContext;

        public FakeCmsRepository(DbContextFakeCms dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetById<T>(int id) where T : BaseEntity
        {
            var entity = await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<List<T>> List<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<int> Add<T>(T entity) where T : BaseEntity
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
