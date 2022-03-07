using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BrainWare.Models
{
    public partial class Order
    {
        public Order()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public int OrderId { get; set; }
        public string Description { get; set; } = null!;
        public int CompanyId { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
    }
}
