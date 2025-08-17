using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public interface ISpecification<T> where T:BaseEntity
    {

        public Expression<Func<T,bool>> Criteria { get; set; }//where(p=>p.Id)
        public List<Expression<Func<T, object>>> Includes { get; set; } // Include(p=>p.User).Include(p=>p.User2)
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

    }
}
