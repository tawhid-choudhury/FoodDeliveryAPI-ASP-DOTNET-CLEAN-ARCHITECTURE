using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Infrastructure.Handlers.Orders
{
    public class GetOrdersByCustomerPhoneHandler : IQueryHandler<GetOrdersByCustomerPhoneQuery, Result<List<OrderDto>>>
    {
        private readonly AppDbContext _db;
        public GetOrdersByCustomerPhoneHandler(AppDbContext db) => _db = db;

        public async Task<Result<List<OrderDto>>> HandleAsync(GetOrdersByCustomerPhoneQuery query, CancellationToken ct = default)
        {
            var orders = await _db.Orders
                .Where(o => o.CustomerPhone == query.Phone)
                .Join(_db.Restaurants, o => o.RestaurantId, r => r.Id, (o, r) => new OrderDto
                {
                    Id = o.Id,
                    Total = o.Total,
                    CreatedAt = o.CreatedAt,
                    RestaurantName = r.Name
                })
                .ToListAsync(ct);

            return Result<List<OrderDto>>.Success(orders);
        }
    }
}
