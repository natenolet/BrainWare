namespace BrainWare.Models
{
    // Legacy objects support the old api
    public class LegacyOrder
    {
        public int OrderId { get; set; }

        public string? CompanyName { get; set; }

        public string? Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<LegacyOrderProduct>? OrderProducts { get; set; }
    }

    public class LegacyOrderProduct
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public LegacyProduct Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }

    public class LegacyProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
