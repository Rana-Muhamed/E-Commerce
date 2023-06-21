using talabat.Core.Entities;

namespace Talabat.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal price { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductBrand { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }
    }
}
