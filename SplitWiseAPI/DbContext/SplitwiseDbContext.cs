using Microsoft.EntityFrameworkCore;
using SplitWiseAPI.Models;

namespace SplitWiseAPI.DbContext
{
    public class SplitwiseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SplitwiseDbContext(DbContextOptions<SplitwiseDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Expense -> User (PaidBy)
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.PaidBy)
                .WithMany(u => u.ExpensesPaid)
                .HasForeignKey(e => e.PaidByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense -> Group
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Expenses)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many: Expense <-> User (SplitAmong)
            modelBuilder.Entity<Expense>()
                .HasMany(e => e.SplitAmong)
                .WithMany(u => u.ExpensesSplit)
                .UsingEntity(j => j.ToTable("ExpenseSplit")); // Optional: custom join table name

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity(j => j.ToTable("UserGroups"));
        }
    }
}
