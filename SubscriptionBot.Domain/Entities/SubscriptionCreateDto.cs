namespace SubscriptionBot.Domain.Entities;

public class SubscriptionCreateDto
{
    public string? Name { get; set; }
    public int ServiceId { get; set; }
    public string? Period { get; set; }
    public int PricingOptionId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}