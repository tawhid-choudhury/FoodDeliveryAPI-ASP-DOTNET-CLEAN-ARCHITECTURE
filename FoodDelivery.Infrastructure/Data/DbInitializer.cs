using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Ensure DB is created
        await context.Database.EnsureCreatedAsync();

        // If already has data, skip
        if (context.Restaurants.Any())
            return;

        // --- Add Restaurants ---
        var restaurant1 = new Restaurant
        {
            Name = "Pizza Planet",
            Address = "123 Space Road"
        };
        var restaurant2 = new Restaurant
        {
            Name = "Burger Galaxy",
            Address = "456 Star Street"
        };

        await context.Restaurants.AddRangeAsync(restaurant1, restaurant2);

        // --- Add Menu Items ---
        var menuItems = new List<MenuItem>
        {
            new MenuItem { RestaurantId = restaurant1.Id, Name = "Margherita Pizza", Price = 8.99m },
            new MenuItem { RestaurantId = restaurant1.Id, Name = "Pepperoni Pizza", Price = 10.50m },
            new MenuItem { RestaurantId = restaurant2.Id, Name = "Classic Burger", Price = 7.25m },
            new MenuItem { RestaurantId = restaurant2.Id, Name = "Cheese Burger", Price = 8.00m }
        };

        await context.MenuItems.AddRangeAsync(menuItems);

        // Save to DB
        await context.SaveChangesAsync();
    }
}
