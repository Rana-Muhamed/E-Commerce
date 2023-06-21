﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Core.Specifications
{
    public class BaseSpecification<T> : Ispecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Skip { get; set ; }
        public int Take { get ; set ; }
        public bool IsPaginationEnabled { get; set ; }

        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)
        {
     
            Criteria = criteriaExpression;
        }
        public void AddOrderBy (Expression<Func<T, object>> OrderByExperssion)
        {
            OrderBy= OrderByExperssion;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExperssion)
        {
            OrderByDescending = OrderByDescExperssion;
        }
        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled= true;
            Skip= skip;
            Take= take;
        }
    }
}