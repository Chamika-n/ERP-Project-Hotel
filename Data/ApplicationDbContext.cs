using Microsoft.EntityFrameworkCore;

namespace GrandHotel.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // චාමිකගේ කිසිම කෝඩ් එකකට හානියක් නැතිව ඔයාගේ ටේබල් එක විතරක් මෙතන හැදෙනවා
        public DbSet<Reservation> Reservations { get; set; }
    }
}