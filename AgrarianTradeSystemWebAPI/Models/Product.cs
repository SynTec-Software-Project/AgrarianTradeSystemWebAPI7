﻿namespace AgrarianTradeSystemWebAPI.Models
{
	public class Product
	{
        public int ProductID { get; set; }

        public string ProductTitle { get; set; }  = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string ProductImage { get; set; } = string.Empty;

        public string ProductType { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

		public double UnitPrice { get; set; }

        public int AvailableStock { get; set; }

        public int MinimumQuantity { get; set;}

        public DateTime DateCreated{ get; set; }

    }
}
