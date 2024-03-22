using System.Net;
using System.Text.Json.Serialization;

namespace AgrarianTradeSystemWebAPI.Models
{
	public class Buyer
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string NIC { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string AddressLineOne { get; set; } = string.Empty;
		public string AddressLineTwo { get; set; } = string.Empty;
		public string AddressLineThree{ get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string ProfileImageUrl { get; set; } = string.Empty;
		[JsonIgnore]
		public Cart? Cart { get; set; }
	}
}
