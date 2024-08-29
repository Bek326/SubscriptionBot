using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionBot.Application;
using SubscriptionBot.Domain;
using SubscriptionBot.Domain.Entities;
using SubscriptionBot.Domain.Interfaces;
using SubscriptionBot.Infrastructure;

namespace SubscriptionBot.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionsController : ControllerBase
{ 
    private readonly ISubscriptionRepository _subscriptionRepository; 
    private readonly IUserRepository _userRepository; 
    private readonly IServiceRepository _serviceRepository; 
    private readonly ILogger<SubscriptionsController> _logger;
    
    public SubscriptionsController(
        ISubscriptionRepository subscriptionRepository, 
        IUserRepository userRepository, 
        IServiceRepository serviceRepository, 
        ILogger<SubscriptionsController> logger)
    { 
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository; 
        _serviceRepository = serviceRepository; 
        _logger = logger;
    }
    
    [HttpGet("{id}")] 
    public async Task<ActionResult<Subscription>> GetSubscriptionById(int id) 
    { 
        _logger.LogInformation($"Fetching subscription with ID: {id}");
        
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(id);
        
        _logger.LogInformation($"Subscription with ID: {id} found."); 
        return Ok(subscription);
    }
    
    [HttpGet("user/{userId}")] 
    public async Task<ActionResult<IEnumerable<Subscription>>> GetUserSubscriptions(int userId) 
    { 
        var subscriptions = await _subscriptionRepository.GetUserSubscriptionsAsync(userId);
        
        return Ok(subscriptions);
    }
    
    [HttpPost] 
    public async Task<ActionResult<Subscription>> AddSubscription(SubscriptionCreateDto subscriptionDto) 
    { 
        var user = await _userRepository.GetUserByIdAsync(subscriptionDto.UserId);
        
        var service = await _serviceRepository.GetServiceByIdAsync(subscriptionDto.ServiceId);
        
        var pricingOption = await _serviceRepository.GetPricingOptionByIdAsync(subscriptionDto.PricingOptionId);
        
        var subscription = new Subscription 
        { 
            Name = subscriptionDto.Name, 
            ServiceId = subscriptionDto.ServiceId, 
            Period = subscriptionDto.Period, 
            PricingOptionId = subscriptionDto.PricingOptionId, 
            UserId = subscriptionDto.UserId, 
            StartDate = subscriptionDto.StartDate, 
            EndDate = subscriptionDto.EndDate, 
            IsActive = subscriptionDto.IsActive
        };
        
        try 
        { 
            await _subscriptionRepository.AddSubscriptionAsync(subscription); 
            return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id }, subscription);
        }
        catch (DbUpdateException ex) 
        { 
            _logger.LogError(ex, "An error occurred while adding the subscription."); 
            return StatusCode(500, "Internal server error.");
        }
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubscription(int id, Subscription subscription)
    {
        if (id != subscription.Id)
        {
            return BadRequest("Subscription ID mismatch.");
        }

        if (!await _subscriptionRepository.SubscriptionExists(id))
        {
            return NotFound("Subscription not found.");
        }

        try
        {
            var existingSubscription = await _subscriptionRepository.GetSubscriptionByIdAsync(id);
        
            if (existingSubscription == null)
            {
                return NotFound("Subscription not found.");
            }
            
            existingSubscription.Name = subscription.Name;
            existingSubscription.ServiceId = subscription.ServiceId;
            existingSubscription.PricingOptionId = subscription.PricingOptionId;
            existingSubscription.UserId = subscription.UserId;
            existingSubscription.StartDate = subscription.StartDate;
            existingSubscription.IsActive = subscription.IsActive;
            existingSubscription.Period = subscription.Period;
            
            existingSubscription.Service = subscription.Service;
            existingSubscription.PricingOption = subscription.PricingOption;
            existingSubscription.User = subscription.User;
            
            await _subscriptionRepository.UpdateSubscriptionAsync(existingSubscription);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!await _subscriptionRepository.SubscriptionExists(id))
            {
                return NotFound("Subscription not found.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating subscription: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")] 
    public async Task<IActionResult> DeleteSubscription(int id) 
    { 
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(id);
        
        if (subscription == null) 
        { 
            return NotFound();
        }
        
        await _subscriptionRepository.DeleteSubscriptionAsync(id);
        
        return NoContent();
    }
}