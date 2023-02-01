using Core.Specification;
using EcommerceAPI.Model;

namespace EcommerceAPI.Specification
{
    public class ProductForCountingSpecification: BaseSpecification<Product>
    {
        public ProductForCountingSpecification(ProductSpecificationParams productParams): 
            base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.Contains(productParams.Search))&&
            (!productParams.Brand.HasValue || x.BrandId == productParams.Brand) &&
            (!productParams.Category.HasValue || x.CategoryId == productParams.Category)
            )
        {

        }
    }
}
