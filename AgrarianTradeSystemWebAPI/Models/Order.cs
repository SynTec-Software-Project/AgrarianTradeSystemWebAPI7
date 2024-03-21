using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgrarianTradeSystemWebAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime OrderedDate { get; set; } = DateTime.Now.Date;
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public double DeliveryFee { get; set; }
       // public Address DeliveryAddress { get; set; }
        public int AddressId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;




        //Link them here
        public ICollection<Assigns> Assigns { get; set; }

        /*
        [Owned]
        public class Address
        {
            [Key]
            public int AddressId { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
        }
        */
    }
}
