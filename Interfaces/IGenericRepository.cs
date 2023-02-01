using Core.Specification;
using EcommerceAPI.Model.PurchaseOrder;
using EcommerceAPI.Models;
using EcommerceAPI.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces
{
    public interface IGenericRepository<T> where T : ClassBase
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdWithSpec(ISpecification<T> spec);
        

        Task<int> CountAsync(ISpecification<T> spec);

        Task<int> Add(T entity);
        Task<int> Update(T entity);

        void AddEntity(T Entity);
        void UpdateEntity(T Entity);
        void DeleteEntity(T Entity);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        
    }
}
