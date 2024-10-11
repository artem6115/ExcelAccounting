using Telegram.Bot;

namespace ExcelAccounting.Loader.Commands
{
    public interface ICommand
    {
        const string ErrorParseCommand = "Не удалось разобрать команду";
        const string ErrorParseValue = "Введена не корректная сумма или процент";
        const string ErrorNegativeRate = "Процентная ставка не может быть отрицательной";
        const string ErrorStashNotFound = "Указаное хранилище не найдено";
        const string ErrorStashHasNotRate = "Указаное хранилище не имеет процентной ставки";
        const string ErrorParseDate = "Введён не корректный номер месяца или дата";
        void Execute(ITelegramBotClient bot,long chatId);

    }
}
