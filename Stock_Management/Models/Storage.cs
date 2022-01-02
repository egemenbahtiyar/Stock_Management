using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Stock_Management.Models
{
    public partial class Storage
    {
        public Storage()
        {
            Employee = new HashSet<Employee>();
            Product = new HashSet<Product>();
        }

        public int StorageId { get; set; }
        public string StorageName { get; set; }
        public string StorageLocation { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
