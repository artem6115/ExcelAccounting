using Telegram.Bot;

namespace ExcelAccounting.Loader.Commands
{
    public interface ICommand
    {
        const string ErrorParseMessage = "Не удалось разобрать команду";
        void Execute(ITelegramBotClient bot,long chatId);

    }
}
