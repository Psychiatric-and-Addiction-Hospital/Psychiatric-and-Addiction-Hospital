using Domain.Entites;
using Domain.Interfaces;
using Domain.Specifications;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Repository_;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AddIdentityDbContext _context;

        public GenericRepository(AddIdentityDbContext context)
        {
            _context = context;
        }
        #region Without spec
        public async Task Add(T item)
     => await _context.Set<T>().AddAsync(item);
        public void Delete(T item)
        => _context.Set<T>().Remove(item);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id); 
        public void Update(T item)
         => _context.Set<T>().Update(item);
        #endregion
        #region With spec
        // _dbContext.Products.Where(p=>p.Id==id).Include(P => P.ProductBrand).Include(P => P.ProductType)
        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            //return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            //return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).ToListAsync();
            return await ApplySpecifications(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }
        #endregion
    }
}
