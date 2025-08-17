using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get  ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set; }
        public int Take { get ; set; }
        public int Skip { get  ; set ; }
        public bool IsPaginationEnabled { get ; set; }

        //Get All
        public BaseSpecifications()
        {
                                    
        }
        //Get By Id
        public BaseSpecifications(Expression<Func<T,bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }                                   
    }

}




