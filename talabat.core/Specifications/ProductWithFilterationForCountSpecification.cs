using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace talabat.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification: BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams specParams) : base(p =>
        (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId.Value) &&
            (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId.Value)
            )
        {

        }
    }
}
