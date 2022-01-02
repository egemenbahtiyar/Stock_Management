using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Stock_Management.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLname { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipCode { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
