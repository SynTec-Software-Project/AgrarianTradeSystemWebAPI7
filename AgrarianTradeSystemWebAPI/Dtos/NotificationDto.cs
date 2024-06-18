using System.ComponentModel.DataAnnotations;

namespace AgrarianTradeSystemWebAPI.Dtos
{
    public class NotificationDto
    {
        [Key]
        public int NotificationId { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime ReceivedTime { get; set; }
        public string? FromCourierID { get; set; }
        public string? ToUserID { get; set; }
        public string? Message { get; set; }
        public int OrderID { get; set; }
        public string? BuyerID { get; set; }
        public string? FarmerID { get; set; }
        public string? CourierID { get; set; }
        public string? OrderStatus { get; set; }
        public string CustomerFName { get; set; } = string.Empty;
        public string CustomerLName { get; set; } = string.Empty;
        public string FarmerFName { get; set; } = string.Empty;
        public string FarmerLName { get; set; } = string.Empty;


    }
}
