using SubscriptionBot.Domain.Entities;

namespace SubscriptionBot.Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetSubscriptionByIdAsync(int id);
    Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(int userId);
    Task AddSubscriptionAsync(Subscription? subscription);
    Task UpdateSubscriptionAsync(Subscription subscription);
    Task DeleteSubscriptionAsync(int id);
    Task<bool> SubscriptionExists(int id);
}
