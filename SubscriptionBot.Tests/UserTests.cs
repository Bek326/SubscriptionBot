using SubscriptionBot.Domain.Entities;

namespace SubscriptionTests;

public class UserTests
{
    [Fact]
    public void User_Initialization_Test()
    {
        var user = new User
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com"
        };
        
        Assert.Equal(1, user.Id);
        Assert.Equal("John Doe", user.Name);
        Assert.Equal("john.doe@example.com", user.Email);
    }
}
