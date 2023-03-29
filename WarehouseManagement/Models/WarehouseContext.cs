using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class WarehouseContext : DbContext
    {
        public WarehouseContext()
        {
        }

        public WarehouseContext(DbContextOptions<WarehouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillLine> BillLines { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<FeatureGroup> FeatureGroups { get; set; }
        public virtual DbSet<Flower> Flowers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupAccount> GroupAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = Warehouse;uid=sa;pwd=123456;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Accid);

                entity.ToTable("Account");

                entity.Property(e => e.Accid).HasColumnName("accid");

                entity.Property(e => e.Pass)
                    .HasMaxLength(10)
                    .HasColumnName("pass")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.PaidDate)
                    .HasColumnType("date")
                    .HasColumnName("paidDate");

                entity.Property(e => e.PaidMoney).HasColumnName("paidMoney");

                entity.Property(e => e.Paymentmethod)
                    .HasMaxLength(50)
                    .HasColumnName("paymentmethod");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Customer");
            });

            modelBuilder.Entity<BillLine>(entity =>
            {
                entity.HasKey(e => new { e.BillId, e.FlowerId })
                    .HasName("PK_BillLine_1");

                entity.ToTable("BillLine");

                entity.Property(e => e.BillId).HasColumnName("billID");

                entity.Property(e => e.FlowerId).HasColumnName("flowerID");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillLines)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillLine_Bill1");

                entity.HasOne(d => d.Flower)
                    .WithMany(p => p.BillLines)
                    .HasForeignKey(d => d.FlowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillLine_Flower1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Isfarmer).HasColumnName("isfarmer");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("department");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("Feature");

                entity.Property(e => e.Fid)
                    .ValueGeneratedNever()
                    .HasColumnName("fid");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<FeatureGroup>(entity =>
            {
                entity.HasKey(e => new { e.Fid, e.Gid });

                entity.ToTable("FeatureGroup");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.FeatureGroups)
                    .HasForeignKey(d => d.Fid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeatureGroup_Feature");

                entity.HasOne(d => d.GidNavigation)
                    .WithMany(p => p.FeatureGroups)
                    .HasForeignKey(d => d.Gid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeatureGroup_Group");
            });

            modelBuilder.Entity<Flower>(entity =>
            {
                entity.ToTable("Flower");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<GroupAccount>(entity =>
            {
                entity.HasKey(e => new { e.Gid, e.Accid });

                entity.ToTable("GroupAccount");

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Accid).HasColumnName("accid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.GroupAccounts)
                    .HasForeignKey(d => d.Accid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupAccount_Account");

                entity.HasOne(d => d.GidNavigation)
                    .WithMany(p => p.GroupAccounts)
                    .HasForeignKey(d => d.Gid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupAccount_Group");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
