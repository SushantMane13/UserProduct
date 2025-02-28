using System;
using System.Collections.Generic;

namespace UserProduct.Services.Models.Entity;

public partial class OrderView
{
    public string UserName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? TotalAmount { get; set; }

    public int DelivaryTypeId { get; set; }

    public string Mode { get; set; } = null!;

    public string Email { get; set; } = null!;
}
