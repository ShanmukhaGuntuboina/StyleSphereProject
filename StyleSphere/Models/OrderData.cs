namespace StyleSphere.Models
{
    public class OrderData
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public string ShippingAddress { get; set; } = null!;

        public string BillingAddress { get; set; } = null!;

        public string TrackingId { get; set; } = null!;

        public decimal NetAmount { get; set; }
    }
}
