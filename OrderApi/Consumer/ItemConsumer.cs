using DedicatedService.Models;
using MassTransit;
using OrderApi.Data;

namespace OrderApi.Consumer
{
    public class ItemConsumer(OrderDbContext dbContext) : IConsumer<Item>
    {
        public async Task Consume(ConsumeContext<Item> context)
        {
            dbContext.Items.Add(context.Message);
            await dbContext.SaveChangesAsync();
        }
    }
}
