using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotel.Models
{
    public class Inventory
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100)]
        public string ItemName { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Minimum stock cannot be negative.")]
        public int MinimumStock { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price cannot be negative.")]
        public decimal UnitPrice { get; set; }

        [StringLength(100)]
        public string Supplier { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}