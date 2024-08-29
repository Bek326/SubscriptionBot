using SubscriptionBot.Domain;
using SubscriptionBot.Domain.Entities;
using SubscriptionBot.Infrastructure;

namespace SubscriptionBot.API.Data;

public static class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Id = 1,
                Name = "Sample User",
                Email = "user@example.com"
            });
        }

        if (!context.Services.Any())
        {
            context.Services.Add(new Service
            {
                Id = 1,
                Name = "Sample Service",
                Description = "Description of Sample Service",
                WeeklyPrice = 10,
                BiweeklyPrice = 18,
                MonthlyPrice = 35,
                QuarterlyPrice = 100,
                SemiAnnualPrice = 190,
                AnnualPrice = 365
            });
        }

        if (!context.PricingOptions.Any())
        {
            context.PricingOptions.Add(new PricingOption
            {
                Id = 1,
                ServiceId = 1, 
                OptionName = "Monthly",
                Price = 35
            });
        }

        if (!context.Subscriptions.Any())
        {
            context.Subscriptions.Add(new Subscription
            {
                Id = 1,
                Name = "Sample Subscription",
                ServiceId = 1, 
                Period = "Monthly",
                PricingOptionId = 1, 
                UserId = 1,
                StartDate = DateTime.Parse("2024-08-15T14:13:56.814Z"),
                EndDate = DateTime.Parse("2024-09-15T14:13:56.814Z"),
                IsActive = true
            });
        }

        context.SaveChanges();
    }
}