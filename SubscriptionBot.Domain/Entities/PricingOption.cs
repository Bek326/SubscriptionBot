using System.Text.Json.Serialization;

namespace SubscriptionBot.Domain.Entities;

public class PricingOption
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public string? OptionName { get; set; }
    public decimal Price { get; set; }
    
    [JsonIgnore]
    public Service? Service { get; set; }
}