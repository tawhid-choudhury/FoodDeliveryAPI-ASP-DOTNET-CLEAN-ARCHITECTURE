using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
using FoodDelivery.Application.Features.Restaurants.DTOs;
using FoodDelivery.Application.Features.Restaurants.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public RestaurantsController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.Query<GetRestaurantsQuery, Result<List<RestaurantDto>>>(new GetRestaurantsQuery());
            return result.Succeeded ? Ok(result.Value) : BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _dispatcher.Query<GetRestaurantByIdQuery, Result<RestaurantDto>>(new GetRestaurantByIdQuery { RestaurantId = id });
            return result.Succeeded ? Ok(result.Value) : NotFound(result.Errors);
        }

        [HttpGet("{id}/menu")]
        public async Task<IActionResult> GetMenu(Guid id)
        {
            var query = new GetMenuQuery { RestaurantId = id };
            var result = await _dispatcher.Query<GetMenuQuery, Result<List<MenuItemDto>>>(query);
            return result.Succeeded ? Ok(result.Value) : NotFound(result.Errors);
        }
    }
}
