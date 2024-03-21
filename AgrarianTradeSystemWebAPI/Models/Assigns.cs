using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrarianTradeSystemWebAPI.Models
{
    public class Assigns
    {
        
        [Key]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public DateTime PickupDate { get; set; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        public Order Order { get; set; }
        
       
    }
}
