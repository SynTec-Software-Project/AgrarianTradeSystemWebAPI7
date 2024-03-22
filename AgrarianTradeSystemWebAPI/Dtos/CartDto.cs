namespace AgrarianTradeSystemWebAPI.Dtos
{
	public class AddToCartRequestDto
	{
		public required string BuyerId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
