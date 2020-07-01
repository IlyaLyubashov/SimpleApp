using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Interfaces
{
    abstract class BaseCrudService<TBase, TCreate, TExist> : ICrudService<TBase, TCreate, TExist>
    {
        public Task<long> Count()
        {
            throw new NotImplementedException();
        }

        public Task<long> Create(TCreate itemDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TBase> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TBase>> List()
        {
            throw new NotImplementedException();
        }

        public Task<List<TExist>> SliceFromOrderedById(int positionFrom, int positionTo)
        {
            throw new NotImplementedException();
        }

        public Task Update(TExist itemDto)
        {
            throw new NotImplementedException();
        }
    }
}
