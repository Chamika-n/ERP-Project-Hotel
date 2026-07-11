using Microsoft.EntityFrameworkCore;
using GrandHotel.Models;

namespace GrandHotel.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}