namespace AgrarianTradeSystemWebAPI.Models.UserModels
{
    public class Farmer : User
    {
        public string CropDetails { get; set; } = string.Empty;
        public string NICFrontImg { get; set; } = string.Empty;
        public string NICBackImg { get; set; } = string.Empty;
        public string GSLetterImg { get; set; } = string.Empty;
    }
}