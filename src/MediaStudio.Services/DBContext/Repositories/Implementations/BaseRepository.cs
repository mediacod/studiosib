using System.Collections.Generic;
using System.Threading.Tasks;
using DBContext.Connect;
using DBContext.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBContext.Repositories.Implementations
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : class
    {
        private MediaStudioContext Context { get; set; }
        public BaseRepository(MediaStudioContext context)
        {
            Context = context;
        }
        public DbSet<TDbModel> GetAll()
        {
            return Context.Set<TDbModel>();
        }

        public async Task<TDbModel> Create(TDbModel model)
        {
            await Context.Set<TDbModel>().AddAsync(model);
            await Context.SaveChangesAsync();
            return model;
        }

        public async Task CreateRange(IEnumerable<TDbModel> entity)
        {
            await Context.Set<TDbModel>().AddRangeAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Update(TDbModel entity)
        {
            Context.Set<TDbModel>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<TDbModel> entity)
        {
            Context.Set<TDbModel>().UpdateRange(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(TDbModel entity)
        {
            Context.Set<TDbModel>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<TDbModel> entity)
        {
            Context.Set<TDbModel>().RemoveRange(entity);
            await Context.SaveChangesAsync();
        }
    }
}
