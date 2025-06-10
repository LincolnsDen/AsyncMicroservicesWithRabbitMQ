using DedicatedService.DTOs;
using DedicatedService.Models;
using ItemApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(IItem itemInterface) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddItem(Item item)
        {
            var response = await itemInterface.AddItemAsync(item);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems() => await itemInterface.GetAllItemAsync();
    }
}
