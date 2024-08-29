using SubscriptionBot.Domain.Entities;

namespace SubscriptionTests;

public class ServiceTests
{
    [Fact]
    public void Service_Initialization_Test()
    {
        var service = new Service
        {
            Id = 1,
            Name = "Streaming Service",
            Description = "Streaming Movies and TV Shows",
            WeeklyPrice = 4.99m,
            BiweeklyPrice = 8.99m,
            MonthlyPrice = 15.99m,
            QuarterlyPrice = 45.99m,
            SemiAnnualPrice = 85.99m,
            AnnualPrice = 150.99m
        };
        
        Assert.Equal(1, service.Id);
        Assert.Equal("Streaming Service", service.Name);
        Assert.Equal("Streaming Movies and TV Shows", service.Description);
        Assert.Equal(4.99m, service.WeeklyPrice);
        Assert.Equal(8.99m, service.BiweeklyPrice);
        Assert.Equal(15.99m, service.MonthlyPrice);
        Assert.Equal(45.99m, service.QuarterlyPrice);
        Assert.Equal(85.99m, service.SemiAnnualPrice);
        Assert.Equal(150.99m, service.AnnualPrice);
    }
}
