using Microsoft.AspNetCore.Mvc;
using SubscriptionBot.Application;
using SubscriptionBot.Application.Services;
using Telegram.Bot.Types;

namespace SubscriptionBot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelegramController : ControllerBase
{
    private readonly TelegramBotService _telegramBotService;

    public TelegramController(TelegramBotService telegramBotService)
    {
        _telegramBotService = telegramBotService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        await _telegramBotService.HandleUpdateAsync(update);
        return Ok();
    }
}