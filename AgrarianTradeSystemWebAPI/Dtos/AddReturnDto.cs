namespace AgrarianTradeSystemWebAPI.Dtos
{
    public class AddReturnDto
    {
        public int OrderID { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

}
