using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.Specifications;

namespace talabat.Repository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, Ispecification<TEntity> spec)
        {
            var query = inputQuery;
            if(spec.Criteria is not null)
                query = query.Where(spec.Criteria);
            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if(spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);
            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);
            query = spec.Includes.Aggregate(query,(currentQuery, inculdeExperssion) => currentQuery.Include(inculdeExperssion));
            return query;
        }
    }
}
