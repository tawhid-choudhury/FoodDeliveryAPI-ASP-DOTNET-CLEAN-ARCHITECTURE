using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Queries
{
    public class GetOrdersByCustomerPhoneQuery
    {
        public required string Phone { get; set; }
    }
}
