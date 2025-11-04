using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Commands
{
    public class CreateOrderCommand
    {
        public required Guid RestaurantId { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerPhone { get; set; }
        public required List<Guid> MenuItemIds { get; set; }
    }
}
