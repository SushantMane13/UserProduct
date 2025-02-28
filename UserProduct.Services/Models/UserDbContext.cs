using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Models;

public partial class UserDbContext : DbContext
{
    public UserDbContext()
    {
    }

    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DelivaryType> DelivaryTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrderView> OrderViews { get; set; }

    public virtual DbSet<PaymentMode> PaymentModes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserEmail> UserEmails { get; set; }

    public virtual DbSet<UserPersonal> UserPersonals { get; set; }

    public virtual DbSet<UserPhone> UserPhones { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LP202;Database=userDB;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DelivaryType>(entity =>
        {
            entity.HasKey(e => e.DelivaryTypeId).HasName("PK_delivaryTypesId");

            entity.ToTable("delivaryTypes");

            entity.Property(e => e.DelivaryTypeId).HasColumnName("delivaryTypeId");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_ordersId");

            entity.ToTable("orders", tb => tb.HasTrigger("updatesellquantity"));

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.DelivaryTypeId).HasColumnName("delivaryTypeId");
            entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusId");
            entity.Property(e => e.PaymentModeId).HasColumnName("paymentModeId");
            entity.Property(e => e.PricePerUnit).HasColumnName("pricePerUnit");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalAmount)
                .HasComputedColumnSql("([quantity]*[pricePerUnit])", false)
                .HasColumnName("totalAmount");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.DelivaryType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DelivaryTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_delivaryTypeId_delivaryTypes_delivaryTypeId");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_orderStatusId_orderStatus_orderStatusId");

            entity.HasOne(d => d.PaymentMode).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_paymentModeId_paymentMode_paymentModeId");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_productId_products_productId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_userId_users_userId");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("PK_orderStatusId");

            entity.ToTable("orderStatus");

            entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusId");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<OrderView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("orderView");

            entity.Property(e => e.DelivaryTypeId).HasColumnName("delivaryTypeId");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Mode)
                .HasMaxLength(41)
                .IsUnicode(false)
                .HasColumnName("mode");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");
            entity.Property(e => e.UserName)
                .HasMaxLength(41)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<PaymentMode>(entity =>
        {
            entity.HasKey(e => e.PaymentModeId).HasName("PK_paymentModeId");

            entity.ToTable("paymentMode");

            entity.Property(e => e.PaymentModeId).HasColumnName("paymentModeId");
            entity.Property(e => e.Mode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mode");
            entity.Property(e => e.SubMode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("subMode");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_productsId");

            entity.ToTable("products", tb => tb.HasTrigger("checkStatusForProducts"));

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductTypeId).HasColumnName("productTypeId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SellQuantity).HasColumnName("sellQuantity");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_products_productTypeId_productType_productTypeId");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("PK_productTypeId");

            entity.ToTable("productType");

            entity.Property(e => e.ProductTypeId).HasColumnName("productTypeId");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.TypeName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("typeName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_usersId");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "UQ_users_username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("changedBy");
            entity.Property(e => e.ChangedOn).HasColumnName("changedOn");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("createdBy");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdOn");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("deletedBy");
            entity.Property(e => e.DeletedOn).HasColumnName("deletedOn");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Salt)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("salt");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.UserAddressId).HasName("PK_userAddressId");

            entity.ToTable("userAddress");


            entity.Property(e => e.UserAddressId).HasColumnName("userAddressId");
            entity.Property(e => e.ChangedBy).HasColumnName("changedBy");
            entity.Property(e => e.ChangedOn).HasColumnName("changedOn");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdOn");
            entity.Property(e => e.DeletedBy).HasColumnName("deletedBy");
            entity.Property(e => e.DeletedOn).HasColumnName("deletedOn");
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");
            entity.Property(e => e.IsPrimary).HasColumnName("isPrimary");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.UserAddressChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_userAddress_changedBy_users_userId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserAddressCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userAddress_createdBy_users_userId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserAddressDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_userAddress_deletedBy_users_userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddressUser)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_userAddress_userId_users_userId");
        });

        modelBuilder.Entity<UserEmail>(entity =>
        {
            entity.ToTable("userEmail");

            entity.HasIndex(e => e.Email, "UQ__userEmai__AB6E616467507872").IsUnique();

            entity.Property(e => e.UserEmailId).HasColumnName("userEmailId");
            entity.Property(e => e.ChangedBy).HasColumnName("changedBy");
            entity.Property(e => e.ChangedOn).HasColumnName("changedOn");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdOn");
            entity.Property(e => e.DeletedBy).HasColumnName("deletedBy");
            entity.Property(e => e.DeletedOn).HasColumnName("deletedOn");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsPrimary).HasColumnName("isPrimary");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.UserEmailChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_userEmail_changedBy_users_userId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserEmailCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userEmail_createdBy_users_userId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserEmailDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_userEmail_deletedBy_users_userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserEmailUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_userEmail_userId_users_userId");
        });

        modelBuilder.Entity<UserPersonal>(entity =>
        {
            entity.HasKey(e => e.UserPersonalId).HasName("PK_userPersonalId");

            entity.ToTable("userPersonal");

            entity.HasIndex(e => e.UserId, "UQ_userPersonal_userId").IsUnique();

            entity.Property(e => e.UserPersonalId).HasColumnName("userPersonalId");
            entity.Property(e => e.ChangedBy).HasColumnName("changedBy");
            entity.Property(e => e.ChangedOn).HasColumnName("changedOn");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdOn");
            entity.Property(e => e.DeletedBy).HasColumnName("deletedBy");
            entity.Property(e => e.DeletedOn).HasColumnName("deletedOn");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Education)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("education");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("middleName");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.UserPersonalChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_userPersonal_changedBy_users_userId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserPersonalCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userPersonal_createdBy_users_userId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserPersonalDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_userPersonal_deletedBy_users_userId");

            entity.HasOne(d => d.User).WithOne(p => p.UserPersonalUser)
                .HasForeignKey<UserPersonal>(d => d.UserId)
                .HasConstraintName("FK_userPersonal_userId_users_userId");
        });

        modelBuilder.Entity<UserPhone>(entity =>
        {
            entity.HasKey(e => e.UserPhoneId).HasName("PK_userPhoneId");

            entity.ToTable("userPhone");

            entity.HasIndex(e => e.Phone, "UQ__userPhon__B43B145F71A08CE1").IsUnique();

            entity.Property(e => e.UserPhoneId).HasColumnName("userPhoneId");
            entity.Property(e => e.ChangedBy).HasColumnName("changedBy");
            entity.Property(e => e.ChangedOn).HasColumnName("changedOn");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdOn");
            entity.Property(e => e.DeletedBy).HasColumnName("deletedBy");
            entity.Property(e => e.DeletedOn).HasColumnName("deletedOn");
            entity.Property(e => e.IsPrimary).HasColumnName("isPrimary");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.UserPhoneChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_userPhone_changedBy_users_userId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserPhoneCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userPhone_createdBy_users_userId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserPhoneDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_userPhone_deletedBy_users_userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserPhoneUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_userPhone_userId_users_userId");
        });

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view1");

            entity.Property(e => e.DelivaryTypeId).HasColumnName("delivaryTypeId");
            entity.Property(e => e.FullName)
                .HasMaxLength(41)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.MaxAmount).HasColumnName("maxAmount");
            entity.Property(e => e.MinAmount).HasColumnName("minAmount");
            entity.Property(e => e.PaymentModeId).HasColumnName("paymentModeId");
            entity.Property(e => e.ProductTypeId).HasColumnName("productTypeId");
            entity.Property(e => e.Rejected).HasColumnName("rejected");
            entity.Property(e => e.TotalOrder).HasColumnName("totalOrder");
            entity.Property(e => e.TotalSpend).HasColumnName("totalSpend");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
