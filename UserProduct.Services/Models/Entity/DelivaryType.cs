using System;
using System.Collections.Generic;

namespace UserProduct.Services.Models.Entity;

public partial class DelivaryType
{
    public int DelivaryTypeId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
