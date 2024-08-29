using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SubscriptionBot.Domain.Entities;

public class Subscription
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("serviceId")]
    public int ServiceId { get; set; }
    
    [JsonPropertyName("period")]
    public string? Period { get; set; }
    
    [JsonPropertyName("pricingOptionId")]
    public int PricingOptionId { get; set; }
    
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("dateTime")]
    public DateTime EndDate { get; set; }
    
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
    
    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; }
    
    [ForeignKey("PricingOptionId")]
    
    public virtual PricingOption PricingOption { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}