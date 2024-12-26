using Microsoft.EntityFrameworkCore;
using MyMoney.Models;
using System.Reflection.Metadata;

namespace MyMoney.Data
{
    public class MyMoneyContext : DbContext
    {
        public MyMoneyContext(DbContextOptions<MyMoneyContext> options)
            : base(options)
        {
        }

        public DbSet<MyMoney.Models.User> User { get; set; } = default!;
        public DbSet<MyMoney.Models.Account> Account { get; set; } = default!;
        public DbSet<MyMoney.Models.AccountType> AccountType { get; set; } = default!;
        public DbSet<MyMoney.Models.Currency> Currency { get; set; } = default!;
        public DbSet<MyMoney.Models.Category> Category { get; set; } = default!;
        public DbSet<MyMoney.Models.Record> Record { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<AccountType>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Category>()
               .Property(u => u.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Currency>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Record>()
               .Property(u => u.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
