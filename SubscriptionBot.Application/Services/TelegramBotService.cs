using System.Diagnostics;
using Newtonsoft.Json;
using SubscriptionBot.Domain.Entities;
using SubscriptionBot.Domain.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SubscriptionBot.Application.Services;

public class TelegramBotService
{
    public ITelegramBotClient BotClient { get; }
    private readonly IServiceRepository _serviceRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public TelegramBotService(string botToken, IServiceRepository serviceRepository, ISubscriptionRepository subscriptionRepository)
    {
        BotClient = new TelegramBotClient(botToken);
        _serviceRepository = serviceRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task HandleUpdateAsync(Update update)
    {
        Console.WriteLine($"Received update: {JsonConvert.SerializeObject(update)}");

        if (update.Message != null && update.Type == UpdateType.Message && update.Message.Type == MessageType.Text)
        {
            var message = update.Message;
            if (message.From != null)
            {
                var userId = message.From.Id;
                var chatId = (int)message.Chat.Id;

                if (message.Text != null)
                    switch (message.Text.ToLower())
                    {
                        case "/start":
                            await BotClient.SendTextMessageAsync(chatId, "Welcome to the Subscription Bot!");
                            break;
                        case "services":
                            await ShowServicesMenu(chatId);
                            break;
                        case "subscriptions":
                            await ShowUserSubscriptions((int)userId, chatId);
                            break;
                        default:
                            await BotClient.SendTextMessageAsync(chatId,
                                "Unknown command. Please choose 'Services' or 'Subscriptions'.");
                            break;
                    }
            }
            else
            {
                await BotClient.SendTextMessageAsync((int)message.Chat.Id, "User information is missing. Please try again.");
            }
        }
        else
        {
            Console.WriteLine($"Unhandled update type: {update.Type}");
        }
    }


    private async Task ShowServicesMenu(int chatId)
    {
        var services = await _serviceRepository.GetAllServicesAsync();
        var serviceList = string.Join("\n", services.Select(s =>
        {
            Debug.Assert(s != null, nameof(s) + " != null");
            return $"{s.Name}: {s.Description}";
        }));
        await BotClient.SendTextMessageAsync(chatId, $"Available Services:\n{serviceList}");
    }

    private async Task ShowUserSubscriptions(int userId, int chatId)
    {
        var subscriptions = await _subscriptionRepository.GetUserSubscriptionsAsync(userId);
        if (subscriptions.Any())
        {
            var subscriptionList = string.Join("\n", subscriptions.Select(s => $"{s.Service.Name}: {s.Period} - {s.EndDate.ToShortDateString()}"));
            await BotClient.SendTextMessageAsync(chatId, $"Your Subscriptions:\n{subscriptionList}");
        }
        else
        {
            await BotClient.SendTextMessageAsync(chatId, "You have no subscriptions.");
        }
    }
}

