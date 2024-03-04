namespace AgrarianTradeSystemWebAPI.Models.UserModels
{
    public class Courier : User
    {
        public string VehicleNo { get; set; } = string.Empty;
        public string VehicleImg { get; set; } = string.Empty;
        public string LicenseImg { get; set; } = string.Empty;
    }
}
