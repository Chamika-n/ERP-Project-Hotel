using System;
using System.ComponentModel.DataAnnotations;

namespace GrandHotel.Models
{
    public class Billing
    {
        [Key]
        public int BillId { get; set; }       

        [Required]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        public string RoomNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // පෙරනිමියෙන් 'Pending' ලෙස තබන්න

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public string PaymentStatus { get; set; } = "Pending";

        public DateTime CheckInDate { get; set; }
        
        
            // ... අනිත් ඒවත් එක්කම මෙන්න මේකත් තියෙන්න ඕනේ ...
     
        

        public string PaymentMethod { get; set; } = "None";
        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}