using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecfications :BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecfications( ProductSpecParams SpecParams)
            :base(p => 
            (string.IsNullOrEmpty(SpecParams.Search) || p.Name.ToLower().Contains(SpecParams.Search))&&
            (!SpecParams.BrandId.HasValue || p.ProductBrandId == SpecParams.BrandId.Value)&&
            (!SpecParams.TypeId.HasValue || p.ProductTypeId == SpecParams.TypeId.Value)
            )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            AddOrderBy(p => p.Name);
            if (!string.IsNullOrEmpty(SpecParams.Sort))
            {
                switch (SpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;  
                }
            }
            ApplyPagination(SpecParams.PageSize*(SpecParams.PageIndex-1), SpecParams.PageSize);
        }
        public ProductWithBrandAndTypeSpecfications(int id): base (P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }


    }
}
