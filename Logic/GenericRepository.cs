using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Model.PurchaseOrder;
using EcommerceAPI.Models;
using EcommerceAPI.Specification;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClassBase
    {
        private readonly MarketDbContext _context;

        public GenericRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // this methods is for implement he Specification

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        // spec is added to the query
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<int> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public void AddEntity(T Entity)
        {
            _context.Set<T>().Add(Entity);

        }

        public void UpdateEntity(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
        }

        public void DeleteEntity(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }

        public Task GetByIdWithSpec(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        
    }
}
