using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBContext.Repositories.Interfaces
{
    public interface IBaseRepository<TDbModel> where TDbModel : class
    {
        DbSet<TDbModel> GetAll();

        Task<TDbModel> Create(TDbModel entity);

        Task CreateRange(IEnumerable<TDbModel> entity);

        Task Update(TDbModel entity);

        Task UpdateRange(IEnumerable<TDbModel> entity);

        Task Delete(TDbModel entity);

        Task DeleteRange(IEnumerable<TDbModel> entity);
    }
}
