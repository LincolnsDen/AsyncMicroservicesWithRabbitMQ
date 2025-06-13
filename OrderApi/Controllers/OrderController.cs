using DedicatedService.DTOs;
using DedicatedService.Models;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Repository;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrder orderInterface) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddOrder(Order order)
        {
            var respone = await orderInterface.AddOrderAsync(order);
            return respone.Flag ? Ok(respone) : BadRequest(respone);
        }
    }
}
