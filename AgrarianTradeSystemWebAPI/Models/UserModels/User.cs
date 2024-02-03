using System.ComponentModel.DataAnnotations;

namespace AgrarianTradeSystemWebAPI.Models.UserModels
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        [Key] public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; } = false;
        public string NIC { get; set; } = string.Empty;
        public string AddL1 { get; set; } = string.Empty;
        public string AddL2 { get; set; } = string.Empty;
        public string AddL3 { get; set;} = string.Empty;
    }
}
