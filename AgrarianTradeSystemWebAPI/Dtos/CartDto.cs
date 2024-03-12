namespace AgrarianTradeSystemWebAPI.Dtos
{
	public class AddToCartRequestDto
	{
		public int BuyerId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
