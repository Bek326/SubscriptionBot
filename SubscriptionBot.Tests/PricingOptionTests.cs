using SubscriptionBot.Domain.Entities;

namespace SubscriptionTests;

public class PricingOptionTests
{
    [Fact]
    public void PricingOption_Initialization_Test()
    {
        var pricingOption = new PricingOption
        {
            Id = 1,
            ServiceId = 1,
            OptionName = "Monthly",
            Price = 9.99m
        };
        
        Assert.Equal(1, pricingOption.Id);
        Assert.Equal(1, pricingOption.ServiceId);
        Assert.Equal("Monthly", pricingOption.OptionName);
        Assert.Equal(9.99m, pricingOption.Price);
    }
}