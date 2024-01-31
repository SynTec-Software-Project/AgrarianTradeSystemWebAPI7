using System.ComponentModel.DataAnnotations;

namespace AgrarianTradeSystemWebAPI.Models
{
    public class UserDto
    {
        //[Key] public int UserId { get; set; }
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
