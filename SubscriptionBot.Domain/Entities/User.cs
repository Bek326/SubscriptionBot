using System.Text.Json.Serialization;

namespace SubscriptionBot.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    
    [JsonIgnore]
    public ICollection<Subscription>? Subscriptions { get; set; }
}