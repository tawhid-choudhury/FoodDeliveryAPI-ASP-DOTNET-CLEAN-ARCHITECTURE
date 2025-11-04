using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.DTOs
{
    public class CreateOrderResponseDto
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
    }
}
