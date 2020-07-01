using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeCMS.BL.Interfaces
{
    public interface ICrudService<TBase, TCreate, TExist>
    {
        Task<TBase> GetById(int id);

        Task<List<TBase>> List();

        Task<long> Create(TCreate itemDto);

        Task Update(TExist itemDto);

        Task DeleteById(int id);

        Task<List<TExist>> SliceFromOrderedById(int positionFrom, int positionTo);

        Task<long> Count();
    }
}
