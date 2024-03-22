using System.Text.Json.Serialization;

namespace AgrarianTradeSystemWebAPI.Models
{
	public class Cart
	{
		public int CartId { get; set; }
		public int BuyerId { get; set; }
		public decimal TotalPrice { get; set; }
		[JsonIgnore]
		public ICollection<CartItem>? CartItems { get; set; }
		[JsonIgnore]
		public Buyer? Buyer { get; set; }

	}
}
