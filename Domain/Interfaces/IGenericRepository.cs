using Domain.Entites;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
     #region Without spec
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task Add(T item);
        void Update(T item);
        void Delete(T item);
        #endregion
        #region With Specifications
          Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
        #endregion


    }
}
