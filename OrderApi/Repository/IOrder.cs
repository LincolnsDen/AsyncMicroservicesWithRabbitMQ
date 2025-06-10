using DedicatedService.DTOs;
using DedicatedService.Models;

namespace OrderApi.Repository
{
    public interface IOrder
    {
        Task<ServiceResponse> AddOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<OrderSummary> GetOrderSummaryAsync();
    }
}
