using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Stock_Management.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime ProductDate { get; set; }
        public bool Stocakable { get; set; }
        public int CategoryId { get; set; }
        public int StorageId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Storage Storage { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
