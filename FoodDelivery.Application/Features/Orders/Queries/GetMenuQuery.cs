using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Queries
{
    public class GetMenuQuery 
    {
        public required Guid RestaurantId { get; set; }
    }
}
