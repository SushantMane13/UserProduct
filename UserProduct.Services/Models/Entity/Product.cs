using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UserProduct.Services.Models.Entity;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int ProductTypeId { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public int SellQuantity { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [JsonIgnore]
    public virtual ProductType ProductType { get; set; } = null!;
}
