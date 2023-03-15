using System;
using System.Collections.Generic;

namespace StyleSphere.Models;

public partial class OrderDetail
{
    public int OrderDetailsId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public int ProductMappingId { get; set; }

    public int OrderId { get; set; }

    public decimal Total { get; set; }

    public bool ActiveStatus { get; set; }

    public virtual OrdersDatum Order { get; set; } = null!;

    public virtual ProductMapping ProductMapping { get; set; } = null!;
}
