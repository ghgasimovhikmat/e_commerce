using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams productPrams)
            : base(x =>
                 (string.IsNullOrEmpty(productPrams.search)||x.Name.ToLower().Contains(productPrams.search))&&
                (!productPrams.BrandId.HasValue || x.ProductBrand.Id == productPrams.BrandId) &&
                (!productPrams.TypeId.HasValue || x.ProductType.Id == productPrams.TypeId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productPrams.PageSize * (productPrams.PageIndex - 1), productPrams.PageSize);

            if (!string.IsNullOrEmpty(productPrams.Sort))
            {
                switch (productPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithTypesAndBrandsSpecification(int id)
        : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
