using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Common.Services;
using FoodDelivery.Application.Features.Orders.Commands;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
using FoodDelivery.Infrastructure.Data;
using FoodDelivery.Infrastructure.Handlers.Menu;
using FoodDelivery.Infrastructure.Handlers.Orders;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=food.db"));

// Dispatcher & CQRS Handlers
builder.Services.AddScoped<IDispatcher, Dispatcher>();
builder.Services.AddScoped<IQueryHandler<GetMenuQuery, Result<List<MenuItemDto>>>, GetMenuHandler>();
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, Result<CreateOrderResponseDto>>, CreateOrderHandler>();

// Controllers & OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// --- SEED DATABASE ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.SeedAsync(dbContext);
}
// ----------------------

// OpenAPI UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
