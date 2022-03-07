using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BrainWare.Models
{
    public partial class Orderproduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
