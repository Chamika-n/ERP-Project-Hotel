using Microsoft.EntityFrameworkCore;

namespace GrandHotel.Models
{
    public class GrandHotelContext : DbContext
    {
        public GrandHotelContext(DbContextOptions<GrandHotelContext> options) : base(options)
        {
        }       

        public DbSet<Billing> Billing { get; set; }

        // මල්ලි, මෙන්න මේ කෑල්ල අපි අලුතින් එකතු කරා. 
        // මේකෙන් Entity Framework එකට කියනවා ඩේටාබේස් එකේ ටේබල් එකේ නම Billings කරන්නේ නැතුව 'Billing' විදිහටම තියාගන්න කියලා.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Billing>().ToTable("Billing");
        }
    }
}