using EcommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable 
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ClassBase;
        Task<int> Complete();
    }
}
