using Microsoft.EntityFrameworkCore;
using SubscriptionBot.Domain.Entities;
using SubscriptionBot.Domain.Interfaces;

namespace SubscriptionBot.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(int id)
    {
        return await _context.Subscriptions
            .Include(s => s.Service)
            .Include(s => s.PricingOption)
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(int userId)
    {
        return await _context.Subscriptions
            .Where(s => s.UserId == userId)
            .Include(s => s.Service)
            .Include(s => s.PricingOption)
            .Include(s => s.User)
            .ToListAsync();
    }

    public async Task AddSubscriptionAsync(Subscription? subscription)
    {
        if (subscription != null) _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateSubscriptionAsync(Subscription subscription)
    {
        var existingSubscription = await _context.Subscriptions
            .Include(s => s.User)
            .Include(s => s.Service)
            .Include(s => s.PricingOption)
            .FirstOrDefaultAsync(s => s.Id == subscription.Id);

        if (existingSubscription == null)
        {
            throw new KeyNotFoundException("Subscription not found");
        }

        existingSubscription.Name = subscription.Name;
        existingSubscription.ServiceId = subscription.ServiceId;
        existingSubscription.PricingOptionId = subscription.PricingOptionId;
        existingSubscription.UserId = subscription.UserId;
        existingSubscription.StartDate = subscription.StartDate;
        existingSubscription.IsActive = subscription.IsActive;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> SubscriptionExists(int id)
    {
        return await _context.Subscriptions.AnyAsync(e => e.Id == id);
    }

    public async Task DeleteSubscriptionAsync(int id)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        if (subscription != null)
        {
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }
    }
}