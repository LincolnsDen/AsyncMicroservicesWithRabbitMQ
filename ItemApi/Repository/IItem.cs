using DedicatedService.DTOs;
using DedicatedService.Models;

namespace ItemApi.Repository
{
    public interface IItem
    {
        Task<ServiceResponse> AddItemAsync(Item item);
        Task<List<Item>> GetAllItemAsync();
    }
}
