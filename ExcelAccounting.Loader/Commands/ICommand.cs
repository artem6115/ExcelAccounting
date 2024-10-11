using Telegram.Bot;

namespace ExcelAccounting.Loader.Commands
{
    public interface ICommand
    {
        const string ErrorParseCommand = "Не удалось разобрать команду";
        const string ErrorParseValue = "Введена не корректная сумма или процент";
        void Execute(ITelegramBotClient bot,long chatId);

    }
}
