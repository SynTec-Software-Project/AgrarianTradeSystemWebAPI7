﻿using System.Text.Json.Serialization;

namespace AgrarianTradeSystemWebAPI.Models
{
    public class Review
    {
       
        public int ReviewId { get; set; }
        public int OrderID { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string ReviewImageUrl { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public int SellerRating { get; set; }
        public int DeliverRating { get; set; }
        public int ProductRating { get; set; }

        [JsonIgnore]
        public Orders? Orders { get; set; }
    }
}
