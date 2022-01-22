using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock_Management.Models
{
    public class HomeIndexViewModel
    {
        public int NoOfOrders { get; set; }
        public int NoOfProducts { get; set; }
        public int NoOfCategories { get; set; }
        public int NoOfCustomers { get; set; }
        public int NoOfStorages { get; set; }
        public int NoOfEmployees { get; set; }
        public Decimal TotalCostOfOrders { get; set; }
        public string TopSellingProduct { get; set; }
        public string LastAddedProduct { get; set; }
        public int EmployeesFromKonya { get; set; }
    }
}
