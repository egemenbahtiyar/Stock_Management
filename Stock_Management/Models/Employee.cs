using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Stock_Management.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLname { get; set; }
        public int? EmployeeAge { get; set; }
        public int StorageId { get; set; }

        public virtual Storage Storage { get; set; }
    }
}
