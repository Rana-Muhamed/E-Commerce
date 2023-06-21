using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.Repositories;

namespace talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
    
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity: BaseEntity;

        Task<int> Compelete();

    }
}
