using AutoMapper;
using talabat.Core.Entities.Order_Aggregate;
using Talabat.DTOs;

namespace Talabat.Helpers
{
    public class OrderItemPicUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {

        private readonly IConfiguration _configuration;

        public OrderItemPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Product.PictureUrl}";
            return string.Empty;
        }
    }
}
