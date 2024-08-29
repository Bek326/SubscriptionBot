using Microsoft.EntityFrameworkCore;
using SubscriptionBot.Domain;
using SubscriptionBot.Domain.Entities;
using SubscriptionBot.Domain.Interfaces;

namespace SubscriptionBot.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service?>> GetAllServicesAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Service?> GetServiceByIdAsync(int serviceId)
    {
        return await _context.Services.FindAsync(serviceId);
    }
    
    public async Task<PricingOption?> GetPricingOptionByIdAsync(int id)
    {
        return await _context.PricingOptions.FindAsync(id);
    }
}