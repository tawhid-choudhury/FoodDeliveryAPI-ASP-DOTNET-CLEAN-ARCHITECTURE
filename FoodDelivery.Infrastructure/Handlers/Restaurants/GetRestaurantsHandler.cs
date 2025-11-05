using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Restaurants.DTOs;
using FoodDelivery.Application.Features.Restaurants.Queries;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Handlers.Restaurants
{
    public class GetRestaurantsHandler : IQueryHandler<GetRestaurantsQuery, Result<List<RestaurantDto>>>
    {
        private readonly AppDbContext db;

        public GetRestaurantsHandler(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Result<List<RestaurantDto>>> HandleAsync(GetRestaurantsQuery query, CancellationToken cancellationToken = default)
        {
            var restaurants = await db.Restaurants
                .Select(r => new RestaurantDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address
                })
                .ToListAsync(cancellationToken);
            return Result<List<RestaurantDto>>.Success(restaurants);
        }
    }
}
