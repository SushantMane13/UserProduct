using System;
using System.Collections.Generic;

namespace UserProduct.Services.Models.Entity;

public partial class PaymentMode
{
    public int PaymentModeId { get; set; }

    public string Mode { get; set; } = null!;

    public string SubMode { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
