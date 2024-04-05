using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithWiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithWiltersForCountSpecification
        (ProductSpecParams productPrams)
            : base(x =>
                (string.IsNullOrEmpty(productPrams.search) || x.Name.ToLower().Contains(productPrams.search)) &&
                (!productPrams.BrandId.HasValue || x.ProductBrand.Id == productPrams.BrandId) &&
                (!productPrams.TypeId.HasValue || x.ProductType.Id == productPrams.TypeId))
        {

        }
    }
}
