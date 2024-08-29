using System.Text.Json.Serialization;

namespace SubscriptionBot.Domain.Entities;

public class Service
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal WeeklyPrice { get; set; }
    public decimal BiweeklyPrice { get; set; }
    public decimal MonthlyPrice { get; set; }
    public decimal QuarterlyPrice { get; set; }
    public decimal SemiAnnualPrice { get; set; }
    public decimal AnnualPrice { get; set; }
    
    [JsonIgnore]
    public ICollection<Subscription> Subscriptions { get; set; }
    public ICollection<PricingOption> PricingOptions { get; set; }
}