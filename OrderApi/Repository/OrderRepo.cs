using DedicatedService.DTOs;
using DedicatedService.Models;
using MassTransit;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using System.Text;

namespace OrderApi.Repository
{
    public class OrderRepo(OrderDbContext context, IPublishEndpoint publishEndpoint) : IOrder
    {
        public async Task<ServiceResponse> AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            var orderSummary = await GetOrderSummaryAsync();
            var emailContent = CreateOrderEmailBody(orderSummary.Id, orderSummary.ItemName, orderSummary.ItemPrice,
                orderSummary.TotalQuantity,orderSummary.TotalAmmount, orderSummary.OrderDate);
            await publishEndpoint.Publish(new EmailDTO("Order Created Sucsessfully", emailContent));
            await ClearOrderTable();
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

        private string CreateOrderEmailBody(int orderId, string itemName, decimal itemPrice,
            int orderQuantity,decimal totalAmmount,DateTime orderDate)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h1><strong>Your Recent Order Information</strong></h1>");
            sb.AppendLine($"<p><strong>Order ID: </strong> {orderId}</p>");
            sb.AppendLine($"<p><strong>Date of Order: </strong> {orderDate}</p>");

            sb.AppendLine("<h2>Order Item : </h2>");
            sb.AppendLine("<ul>");

            sb.AppendLine($"<li> Product Name: {itemName} </li>");
            sb.AppendLine($"<li> Product Price: {itemPrice} </li>");
            sb.AppendLine($"<li> Purchase Quantity: {orderQuantity} </li>");
            sb.AppendLine($"<li> Total Ammount: {totalAmmount} </li>");

            sb.AppendLine("</ul>");

            sb.AppendLine("<p> Thank You for your Order.</p>");
            return sb.ToString();
        }

        private async Task ClearOrderTable()
        {
            context.Orders.Remove(await context.Orders.FirstOrDefaultAsync());
            await context.SaveChangesAsync();
        }
    }
}
