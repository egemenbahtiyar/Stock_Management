using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Stock_Management.Models
{
    public partial class Order
    {
        public Order()
        {
            Customer = new HashSet<Customer>();
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual OrderDetails OrderDetails { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
