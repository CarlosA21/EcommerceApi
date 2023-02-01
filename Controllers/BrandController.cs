using EcommerceAPI.Interfaces;
using EcommerceAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    public class BrandController :BaseApiController
    {
        private readonly IGenericRepository<Brand> _brandRepository;
        public BrandController(IGenericRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetAllBrand()
        {
            return Ok(await _brandRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandById(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }
    }
}
