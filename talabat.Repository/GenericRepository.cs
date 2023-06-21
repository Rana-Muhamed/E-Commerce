using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.Repositories;
using talabat.Core.Specifications;
using talabat.Repository.Data;

namespace talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if(typeof(T) == typeof(Product)) 
            //    return (IEnumerable<T>) await _dbcontext.Products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
            //else
                return await _dbcontext.Set<T>().ToListAsync();
        }

    
        public async Task<T> GetByIdAsync(int id)
        {
            // _dbcontext.Products.Where(X => X.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();
            //return await _dbcontext.Set<T>().Where(X => X.Id == id).FirstOrDefaultAsync();
            return await _dbcontext.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(Ispecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByEntityWithSpecAsync(Ispecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountWithSpecAsync(Ispecification<T> spec)
        {
          return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecification(Ispecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(), spec);
        }

        public async Task Add(T entity)
         => await _dbcontext.Set<T>().AddAsync(entity);

        public void Update(T entity)
             =>  _dbcontext.Set<T>().Update(entity);

        public void Delete(T entity)
             => _dbcontext.Set<T>().Remove(entity);

    }
}
