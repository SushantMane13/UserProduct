using System;
using System.Collections.Generic;

namespace UserProduct.Services.Models.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateOnly CreatedOn { get; set; }

    public string? ChangedBy { get; set; }

    public DateOnly? ChangedOn { get; set; }

    public string? DeletedBy { get; set; }

    public DateOnly? DeletedOn { get; set; }

    public string Salt { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<UserAddress> UserAddressChangedByNavigations { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserAddress> UserAddressCreatedByNavigations { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserAddress> UserAddressDeletedByNavigations { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserAddress>? UserAddressUser { get; set; }

    public virtual ICollection<UserEmail> UserEmailChangedByNavigations { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserEmail> UserEmailCreatedByNavigations { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserEmail> UserEmailDeletedByNavigations { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserEmail> UserEmailUsers { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserPersonal> UserPersonalChangedByNavigations { get; set; } = new List<UserPersonal>();

    public virtual ICollection<UserPersonal> UserPersonalCreatedByNavigations { get; set; } = new List<UserPersonal>();

    public virtual ICollection<UserPersonal> UserPersonalDeletedByNavigations { get; set; } = new List<UserPersonal>();

    public virtual UserPersonal? UserPersonalUser { get; set; }

    public virtual ICollection<UserPhone> UserPhoneChangedByNavigations { get; set; } = new List<UserPhone>();

    public virtual ICollection<UserPhone> UserPhoneCreatedByNavigations { get; set; } = new List<UserPhone>();

    public virtual ICollection<UserPhone> UserPhoneDeletedByNavigations { get; set; } = new List<UserPhone>();

    public virtual ICollection<UserPhone> UserPhoneUsers { get; set; } = new List<UserPhone>();
}
