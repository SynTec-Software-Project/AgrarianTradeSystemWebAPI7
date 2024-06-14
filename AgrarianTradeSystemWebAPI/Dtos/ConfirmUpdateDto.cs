using System.ComponentModel.DataAnnotations;

namespace AgrarianTradeSystemWebAPI.Dtos
{
    public class ConfirmUpdateDto
    {
        [Key]
        public int OrderID { get; set; }
        public string NewStatus { get; set; }
    }
}
