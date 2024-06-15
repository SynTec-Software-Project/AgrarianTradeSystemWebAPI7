namespace AgrarianTradeSystemWebAPI.Dtos
{
    public class ReturnDto
    {
        public int ReturnId { get; set; }
        public int OrderID { get; set; }
        public string? ProductTitle { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public string? ProductImageUrl { get; set; } = string.Empty;
        public string? Reason { get; set; } = string.Empty;
        public string? ReturnImageUrl { get; set; } = string.Empty;
        public DateTime ReturnDate { get; set; } = DateTime.Now;
       
    }
}
