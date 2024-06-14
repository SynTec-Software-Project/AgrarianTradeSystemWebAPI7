using AgrarianTradeSystemWebAPI.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AgrarianTradeSystemWebAPI.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime ReceivedTime { get; set; }
        public string? FromCourierID { get; set; }
        public string? ToUserID { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public User? Buyer { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        [JsonIgnore]
        public Courier? Courier { get; set; }
        [JsonIgnore]
        public Orders? Orders { get; set; }

    }
}
