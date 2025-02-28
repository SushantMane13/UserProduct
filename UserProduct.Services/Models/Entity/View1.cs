using System;
using System.Collections.Generic;

namespace UserProduct.Services.Models.Entity;

public partial class View1
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int? TotalOrder { get; set; }

    public int? TotalSpend { get; set; }

    public int? MinAmount { get; set; }

    public int? MaxAmount { get; set; }

    public int ProductTypeId { get; set; }

    public int PaymentModeId { get; set; }

    public int DelivaryTypeId { get; set; }

    public bool? Rejected { get; set; }
}
