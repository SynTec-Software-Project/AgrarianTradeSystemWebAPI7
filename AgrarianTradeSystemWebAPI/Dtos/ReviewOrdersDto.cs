namespace AgrarianTradeSystemWebAPI.Dtos
{
	public class ReviewOrdersDto
	{
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string?ProductDescription { get; set; }
        public string? ProductImageUrl { get; set; }

    }
}
