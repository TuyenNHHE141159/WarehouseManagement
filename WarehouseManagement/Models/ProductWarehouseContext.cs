using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class ProductWarehouseContext : DbContext
    {
        public ProductWarehouseContext()
        {
        }

        public ProductWarehouseContext(DbContextOptions<ProductWarehouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountRole> AccountRoles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleFeature> RoleFeatures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = ProductWarehouse;uid=sa;pwd=123456;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.Username, "uq_username")
                    .IsUnique();

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("username")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AccountRole>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.RoleId });

                entity.ToTable("Account_Role");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountRoles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role_Account");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AccountRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role_Role");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Customer_Account");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("Feature");

                entity.Property(e => e.Fid)
                    .ValueGeneratedNever()
                    .HasColumnName("fid");

                entity.Property(e => e.Feature1)
                    .HasMaxLength(10)
                    .HasColumnName("feature")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasColumnName("note");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderType)
                    .HasMaxLength(10)
                    .HasColumnName("order_type")
                    .IsFixedLength(true);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("Order_Product");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Product_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Product_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(10)
                    .HasColumnName("role_name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<RoleFeature>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FeatureId });

                entity.ToTable("Role_Feature");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.FeatureId).HasColumnName("feature_id");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.RoleFeatures)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Feature_Feature");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleFeatures)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Feature_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
