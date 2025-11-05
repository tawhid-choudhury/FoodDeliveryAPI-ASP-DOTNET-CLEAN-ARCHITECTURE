using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Orders.Commands;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public OrdersController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _dispatcher.Send<CreateOrderCommand, Result<CreateOrderResponseDto>>(command);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetOrder), new { id = result.Value!.OrderId }, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            // would implement a GetOrderQuery and query handler similarly
            return Ok();
        }

        [HttpGet("customer/{phone}")]
        public async Task<IActionResult> GetOrdersByCustomer(string phone)
        {
            var query = new GetOrdersByCustomerPhoneQuery { Phone = phone };
            var result = await _dispatcher.Query<GetOrdersByCustomerPhoneQuery, Result<List<OrderDto>>>(query);
            return result.Succeeded ? Ok(result.Value) : NotFound(result.Errors);
        }
    }
}
