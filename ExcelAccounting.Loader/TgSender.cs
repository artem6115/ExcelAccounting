using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ExcelAccounting.Loader
{
    public abstract class TgSender
    {
        public async Task SendMessageAsync(ITelegramBotClient bot, long chatId,string message) {
            try
            {
                await bot.SendTextMessageAsync(chatId, message, replyMarkup: TgBot.ButtonsMenu());
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
