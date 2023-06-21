using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Order_Aggregate;

namespace talabat.Core.Specifications.Order_Spec
{
    public class OrderWithPaymentIntentIdSpecifications :BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId):
            base(O => O.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
