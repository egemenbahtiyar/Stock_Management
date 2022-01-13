using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock_Management.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductId { get; set; }
        public int Quantitiy { get; set; }
        public decimal? TotalCost { get; set; }


    }
}
