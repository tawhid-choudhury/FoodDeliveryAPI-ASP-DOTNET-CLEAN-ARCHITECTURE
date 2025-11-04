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

namespace FoodDelivery.Infrastructure.Handlers.Menu
{
    public class GetMenuHandler : IQueryHandler<GetMenuQuery, Result<List<MenuItemDto>>>
    {
        private readonly AppDbContext _db;

        public GetMenuHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<List<MenuItemDto>>> HandleAsync(GetMenuQuery query, CancellationToken cancellationToken = default)
        {
            var items = await _db.MenuItems
                .Where(mi => mi.RestaurantId == query.RestaurantId)
                .Select(mi => new MenuItemDto { Id = mi.Id, Name = mi.Name, Price = mi.Price })
                .ToListAsync(cancellationToken);

            return Result<List<MenuItemDto>>.Success(items);
        }
    }
}
