using DedicatedService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemApi.Data
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
    }
}
