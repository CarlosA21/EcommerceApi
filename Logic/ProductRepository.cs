
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketDbContext _context;
        public ProductRepository(MarketDbContext context)
        {
            _context = context;
        }
        // With this can see all products. Interface IProductRepository is being used
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();
        }
        // With this can see the products by id. Interface IProductRepository is being used
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Product
                .FirstOrDefaultAsync(p => p.Id == id);

        }
    }
}
