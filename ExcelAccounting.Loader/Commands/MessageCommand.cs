using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExcelAccounting.Loader.Commands
{
    public class MessageCommand : TgSender ,ICommand
    {
        string _message;
        public MessageCommand(string message) => _message = message;
        public async void Execute(ITelegramBotClient bot, long chatId)
        {
           await SendMessageAsync(bot, chatId,_message);
        }
    }
}
