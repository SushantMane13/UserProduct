﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UserProduct.Services.Models.Entity;

public partial class UserPhone
{
    public int UserPhoneId { get; set; }

    public int UserId { get; set; }

    public string Phone { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateOnly CreatedOn { get; set; }

    public int? ChangedBy { get; set; }

    public DateOnly? ChangedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateOnly? DeletedOn { get; set; }

    public bool IsPrimary { get; set; }

    [JsonIgnore]
    public virtual User? ChangedByNavigation { get; set; }

    [JsonIgnore]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual User? DeletedByNavigation { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
