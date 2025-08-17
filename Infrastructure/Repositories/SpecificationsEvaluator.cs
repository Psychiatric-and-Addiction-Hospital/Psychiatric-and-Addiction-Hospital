using Domain.Entites;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 

namespace Talabat.Repository_
{
    public static class SpecificationsEvaluator<T>where T : BaseEntity
    {
        // _dbContext.Set<T>.Where(p=>p.Id==id).Include(P => P.ProductBrand).Include(P => P.ProductType)

        public static  IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var Query = inputQuery;// _dbContext.Set<T>

            if(spec.Criteria is not null )
            {
                Query = Query.Where(spec.Criteria);//p=>p.Id==id
            }
            //Add Order By Expression
            if(spec.OrderBy is not null)//p=>p.Name
             Query= Query.OrderBy(spec.OrderBy);//OrderBy(p=>p.Name)
            if(spec.OrderByDescending is not null)
                Query=Query.OrderByDescending(spec.OrderByDescending);////OrderByDescending(p=>p.Name)

            if(spec.IsPaginationEnabled)
            {
                Query=Query.Skip(spec.Skip).Take(spec.Take);
            }

            Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) =>
            CurrentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}
