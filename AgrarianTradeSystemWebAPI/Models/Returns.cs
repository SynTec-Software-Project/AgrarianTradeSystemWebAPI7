using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AgrarianTradeSystemWebAPI.Models
{
	public class Returns
	{
		[Key]
        public int ReturnID { get; set; }
		public int OrderID { get; set; }
		public required string Reason { get; set; }
		public DateTime ReturnDate { get; set;}
        public string? ReturnImageUrl { get; set; }
		[JsonIgnore]
		public Orders? Orders{ get; set;}

    }
}
