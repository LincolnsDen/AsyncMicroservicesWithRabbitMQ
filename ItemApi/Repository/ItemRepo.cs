using DedicatedService.DTOs;
using DedicatedService.Models;
using ItemApi.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ItemApi.Repository
{
    public class ItemRepo(ItemDbContext context,IPublishEndpoint publishEndpoint) : IItem
    {
        public async Task<ServiceResponse> AddItemAsync(Item item)
        {
            context.Items.Add(item);
            await context.SaveChangesAsync();
            await publishEndpoint.Publish(item);
            return new ServiceResponse(true, "Item Added Successfully");
        }

        public async Task<List<Item>> GetAllItemAsync()
        {
            var items = await context.Items.ToListAsync();
            return items;
        }
    }
}
