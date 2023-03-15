namespace StyleSphere.Models
{
    public class CheckoutMaster
    {
        public CheckoutMaster()
        {
            OrderDetails = new List<Checkout>();
        }
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public string ShippingAddress { get; set; } = null!;

        public string BillingAddress { get; set; } = null!;

        public string TrackingId { get; set; } = null!;

        public decimal NetAmount { get; set; }

        public bool ActiveStatus { get; set; }

        public virtual ICollection<Checkout> OrderDetails { get; set; }
    }
}