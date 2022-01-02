using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Stock_Management.Models
{
    public partial class OrderDetails
    {
        public int OrderId { get; set; }
        public int Quantitiy { get; set; }
        public decimal? TotalCost { get; set; }

        public virtual Order Order { get; set; }
    }
}
