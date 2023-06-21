using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Order_Aggregate;

namespace talabat.Core.Specifications.Order_Spec
{
    public class OrderSpecifications: BaseSpecification<Order>
    {
        public OrderSpecifications(string email):
            base(O => O.BuyerEmail == email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDesc(O => O.OrderDate); 
        }
        public OrderSpecifications(string email, int orderId) :
           base(O => O.BuyerEmail == email && O.Id == orderId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

        }
    }
}
