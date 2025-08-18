using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly AddIdentityDbContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(AddIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
           => await _dbContext.DisposeAsync();
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;//Key===>Product ,Order
            if (!_repositories.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(_dbContext);//Value
                _repositories.Add(type, Repository);
            }

            //return   (IGenericRepository < TEntity >) _repositories[type];
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
