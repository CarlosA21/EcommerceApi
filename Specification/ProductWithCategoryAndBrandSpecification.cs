using Core.Specification;
using EcommerceAPI.Model;

namespace EcommerceAPI.Specification
{
    public class ProductWithCategoryAndBrandSpecification : BaseSpecification<Product>
    {
        //All product with the relationship of product, brand and category.
        public ProductWithCategoryAndBrandSpecification(ProductSpecificationParams productParams)
            : base (x =>
            (string.IsNullOrEmpty(productParams.Search)|| x.Name.Contains(productParams.Search)) &&
            (!productParams.Brand.HasValue || x.BrandId == productParams.Brand) &&
            (!productParams.Category.HasValue || x.CategoryId == productParams.Category)
            )
            
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Brand);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "descriptionAsc":
                        AddOrderBy(p => p.Description);
                        break;
                    case "descriptionDesc":
                        AddOrderByDescending(p => p.Description);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithCategoryAndBrandSpecification(int id): base(x => x.BrandId == id)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Brand);
        }
    }
}
