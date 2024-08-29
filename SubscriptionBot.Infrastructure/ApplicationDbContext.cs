using Microsoft.EntityFrameworkCore;
using SubscriptionBot.Domain;
using SubscriptionBot.Domain.Entities;

namespace SubscriptionBot.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Service> Services { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<PricingOption> PricingOptions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>()
            .HasMany(s => s.Subscriptions)
            .WithOne(sub => sub.Service)
            .HasForeignKey(sub => sub.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Service>()
            .HasMany(s => s.PricingOptions)
            .WithOne(po => po.Service)
            .HasForeignKey(po => po.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Subscription>()
            .HasOne(sub => sub.Service)
            .WithMany(s => s.Subscriptions)
            .HasForeignKey(sub => sub.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PricingOption>()
            .HasOne(po => po.Service)
            .WithMany(s => s.PricingOptions)
            .HasForeignKey(po => po.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Subscription>()
            .HasOne(sub => sub.User)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(sub => sub.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}