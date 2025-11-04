using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Orders.Commands;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Infrastructure.Handlers.Orders
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, Result<CreateOrderResponseDto>>
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(AppDbContext db,ILogger<CreateOrderHandler> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Result<CreateOrderResponseDto>> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            var menuItems = await _db.MenuItems
                .Where(mi => command.MenuItemIds.Contains(mi.Id))
                .ToListAsync(cancellationToken);

            if(!menuItems.Any())
            {
                _logger.LogWarning("No valid menu items found for the provided IDs.");
                return Result<CreateOrderResponseDto>.Failure("No valid menu items found.");
            }

            var total = menuItems.Sum(mi => mi.Price);

            var order = new Order
            {
                RestaurantId = command.RestaurantId,
                CustomerName = command.CustomerName,
                CustomerPhone = command.CustomerPhone,
                Total = total,
                CreatedAt = DateTime.UtcNow
            };

            await _db.Orders.AddAsync(order, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            var dto = new CreateOrderResponseDto
            {
                OrderId = order.Id,
                Total = order.Total,
            };

            return Result<CreateOrderResponseDto>.Success(dto);
        }
    }
}
