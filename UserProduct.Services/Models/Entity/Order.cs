using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UserProduct.Services.Models.Entity;

public partial class Order
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public int PricePerUnit { get; set; }

    public int? TotalAmount { get; set; }

    public int DelivaryTypeId { get; set; }

    public int PaymentModeId { get; set; }

    public int OrderStatusId { get; set; }

    [JsonIgnore]
    public virtual DelivaryType DelivaryType { get; set; } = null!;

    [JsonIgnore]
    public virtual OrderStatus OrderStatus { get; set; } = null!;

    [JsonIgnore]
    public virtual PaymentMode PaymentMode { get; set; } = null!;

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
