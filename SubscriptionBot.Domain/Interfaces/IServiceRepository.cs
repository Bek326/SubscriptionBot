using SubscriptionBot.Domain.Entities;

namespace SubscriptionBot.Domain.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service?>> GetAllServicesAsync();
    Task<Service?> GetServiceByIdAsync(int serviceId);
    Task<PricingOption?> GetPricingOptionByIdAsync(int id);
}