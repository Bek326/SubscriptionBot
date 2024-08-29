using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubscriptionBot.Application.Services;
using Telegram.Bot;

namespace SubscriptionBot.Application.Services;

public class TelegramBotBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TelegramBotBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var telegramBotService = scope.ServiceProvider.GetRequiredService<TelegramBotService>();

            telegramBotService.BotClient.StartReceiving(
                updateHandler: async (botClient, update, ct) => await telegramBotService.HandleUpdateAsync(update),
                pollingErrorHandler: (botClient, exception, ct) =>
                {
                    Console.WriteLine(exception);
                    return Task.CompletedTask;
                },
                cancellationToken: stoppingToken
            );

            await Task.Delay(-1, stoppingToken);
        }
    }
}

