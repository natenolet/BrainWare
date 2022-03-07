using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BrainWare.Models
{
    public partial class Product
    {
        public Product()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Price { get; set; }
        [JsonIgnore]
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
    }
}
