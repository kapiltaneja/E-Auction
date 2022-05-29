using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DomainModel.Model;

namespace DataAccessSqliteProvider
{
    // >dotnet ef migration add testMigration
    public class DomainModelSqliteContext : DbContext
    {
        public DomainModelSqliteContext(DbContextOptions<DomainModelSqliteContext> options) : base(options)
        { }

        public DbSet<DataEventRecord> DataEventRecords { get; set; }

        public DbSet<SourceInfo> SourceInfos { get; set; }
        public DbSet<SellerInfo> SellerInfo { get; set; }
        public DbSet<BuyerInfo> BuyerInfo { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            builder.Entity<SellerInfo>().HasKey(m => m.SellerId);
            builder.Entity<BuyerInfo>().HasKey(m => m.BuyerId);
            builder.Entity<ProductInfo>().HasKey(m => m.ProductId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();

            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}