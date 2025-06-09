using DedicatedService.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
