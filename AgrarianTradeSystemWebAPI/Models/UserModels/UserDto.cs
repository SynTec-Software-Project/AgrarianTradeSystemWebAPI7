using System.ComponentModel.DataAnnotations;

namespace AgrarianTradeSystemWebAPI.Models.UserModels
{
    public class UserDto
    {
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string NICnumber { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string AddressLine3 { get; set; } = string.Empty;
        public string ProfileImg { get; set;} = string.Empty;
    }
}
