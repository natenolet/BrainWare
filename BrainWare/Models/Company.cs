using System;
using System.Collections.Generic;

namespace BrainWare.Models
{
    public partial class Company
    {
        public Company()
        {
            Orders = new HashSet<Order>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
