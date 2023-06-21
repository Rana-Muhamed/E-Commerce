using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Core.Entities;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.Repositories;
using talabat.Repository.Data;

namespace talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repositories;


        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null )
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;


        }
        public async Task<int> Compelete()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();


    }
}
