using DedicatedService.DTOs;
using DedicatedService.Models;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;

namespace OrderApi.Repository
{
    public class OrderRepo(OrderDbContext context) : IOrder
    {
        public async Task<ServiceResponse> AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            var orderSummary = await GetOrderSummaryAsync();
            return new ServiceResponse(true, "Order Created Successfully");
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var order = await context.Orders.ToListAsync();
            return order;
        }

        public async Task<OrderSummary> GetOrderSummaryAsync()
        {
            var order = await context.Orders.FirstOrDefaultAsync();
            var items = await context.Items.ToListAsync();
            var itemInfo = items.Find(x => x.Id == order!.ItemId);
            return new OrderSummary
            (
                order!.Id,
                order.ItemId,
                itemInfo!.Name!,
                itemInfo.Price,
                order.Quantity,
                order.Quantity * itemInfo.Price,
                order.OrderDate
            );
        }
    }
}
