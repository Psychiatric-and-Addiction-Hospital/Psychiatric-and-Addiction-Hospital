using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        Task<int> CompleteAsync();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    }
}
