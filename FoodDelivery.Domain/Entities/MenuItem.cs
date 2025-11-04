using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Entities
{
    public class MenuItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
