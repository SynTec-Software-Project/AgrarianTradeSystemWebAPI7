namespace AgrarianTradeSystemWebAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Message { get; set; }
        public DateTime? SendAt { get; set; }=DateTime.Now;
        public bool? Seen { get; set; }
    }
}
