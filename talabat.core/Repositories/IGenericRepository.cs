using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.Specifications;

namespace talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity//means only class that inherit from BaseEntity can implement that interface
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(Ispecification<T> spec);
        Task<T> GetByEntityWithSpecAsync(Ispecification<T> spec);
        Task<int> GetCountWithSpecAsync(Ispecification<T> spec);
        Task Add (T entity);
        void Update (T entity);
        void Delete (T entity);
    }
}
