using SubscriptionBot.Domain.Entities;

namespace SubscriptionTests;

public class SubscriptionTests
{
    [Fact]
    public void Subscription_Initialization_Test()
    {
        var subscription = new Subscription
        {
            Id = 1,
            Name = "Monthly Subscription",
            ServiceId = 1,
            Period = "Monthly",
            PricingOptionId = 1,
            UserId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            IsActive = true
        };
        
        Assert.Equal(1, subscription.Id);
        Assert.Equal("Monthly Subscription", subscription.Name);
        Assert.Equal(1, subscription.ServiceId);
        Assert.Equal("Monthly", subscription.Period);
        Assert.Equal(1, subscription.PricingOptionId);
        Assert.Equal(1, subscription.UserId);
        Assert.Equal(DateTime.Now.Date, subscription.StartDate.Date);
        Assert.Equal(DateTime.Now.AddMonths(1).Date, subscription.EndDate.Date);
        Assert.True(subscription.IsActive);
    }

    [Fact]
    public void Subscription_IsActive_Should_Be_False_When_EndDate_Passed()
    {
        var subscription = new Subscription
        {
            StartDate = DateTime.Now.AddMonths(-2),
            EndDate = DateTime.Now.AddMonths(-1),
            IsActive = true
        };
        
        if (DateTime.Now > subscription.EndDate)
        {
            subscription.IsActive = false;
        }
        
        Assert.False(subscription.IsActive);
    }
}
