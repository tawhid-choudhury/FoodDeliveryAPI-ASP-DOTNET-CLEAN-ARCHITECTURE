using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Restaurants.DTOs;
using FoodDelivery.Application.Features.Restaurants.Queries;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace FoodDelivery.Infrastructure.Handlers.Restaurants
{
    public class GetRestaurantByIdHandler : IQueryHandler<GetRestaurantByIdQuery, Result<RestaurantDto>>
    {
        private readonly AppDbContext _db;
        public GetRestaurantByIdHandler(AppDbContext db) => _db = db;

        public async Task<Result<RestaurantDto>> HandleAsync(GetRestaurantByIdQuery query, CancellationToken ct = default)
        {
            var restaurant = await _db.Restaurants
                .Where(r => r.Id == query.RestaurantId)
                .Select(r => new RestaurantDto { Id = r.Id, Name = r.Name, Address = r.Address })
                .FirstOrDefaultAsync(ct);

            return restaurant == null
                ? Result<RestaurantDto>.Failure("Restaurant not found")
                : Result<RestaurantDto>.Success(restaurant);
        }
    }
}
