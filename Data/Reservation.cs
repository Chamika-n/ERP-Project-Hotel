using System;
using System.ComponentModel.DataAnnotations;

namespace GrandHotel.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Room Number is required")]
        public string RoomNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}