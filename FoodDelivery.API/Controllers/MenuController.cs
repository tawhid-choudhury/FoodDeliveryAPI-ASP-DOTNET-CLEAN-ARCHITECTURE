using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public MenuController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetMenu(Guid restaurantId)
        {
            var query = new GetMenuQuery { RestaurantId = restaurantId };
            var result = await _dispatcher.Query<GetMenuQuery, Result<List<MenuItemDto>>>(query);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok(result.Value);
        }
    }
}
