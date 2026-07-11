using System.ComponentModel.DataAnnotations;

namespace GrandHotel.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } // Available or Booked
    }
}
