using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExcelAccounting.Loader
{
    public static class TgBot
    {
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine();
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if (update.Message.Type != MessageType.Text) return;
                var command = CommandActivator.CreateCommand(update.Message.Text);
                command.Execute(botClient, update.Message.Chat.Id);
            }

        }
        public static IReplyMarkup ButtonsMenu()
        {
            return new ReplyKeyboardMarkup
            (
                new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton ("/Start"),new KeyboardButton ("/About") },
                    new List<KeyboardButton>{ new KeyboardButton ("/Format"),new KeyboardButton ("/Help") },
                    new List<KeyboardButton>{ new KeyboardButton ("/Report"), new KeyboardButton("/Rate") }


                }
            );
        }

        public const string _sartText = "Бот запущен, для получения информации о командах\nвведите: '/help' \nПриятного пользования!";
        public const string _formatText = "Есть несколько вариантов сделать запись:\n" +
                                         "Формат1: [Счёт];[Сумма]\n" +
                                         "Формат2: [Счёт];[Группа];[Сумма]\n" +
                                         "Формат3: [Счёт];[Группа];[Подгруппа];[Описание];[Сумма]\n" +
                                         "Если это расход пишется со знаком '-'\n" +
                                         "Пример: Альфа банк;Еда;;Кофе;-300\n";
        public const string _aboutText = "Это небольшая программа для загрузки данных из бота и " +
                                        "их наглядным отображением в excel документе.\nРазработанна для простого, быстрого и удобного способа хранения финансовой информации.\n" +
                                        "Что бы добавить запись нужно написать боту в соответствии с форматом.\n" +
                                         "Все файлы программы находятся в паке Data.\n" +
                                         "О формате записи можно узнать у бота написав '/help'\n" +
            "Автор программы: Миков Артём Андреевич\n" +
            "https://vk.com/mikov2003 \n";
        public const string _helpText = "Перечень команд:\n" +
                                 "/start: приветственная информация\n" +
                                "/about: о программе \n" +
                                "/format: формат ввода транзакицй\n" +
                                "/rate: начисление процентов на сберигательный счёт\n" +
                                "/report: отчёт за месяц \n" +
                                "/stash: отложить деньги\n" +
                                "Отсутсвие команды - новая транзакция (наберите команду /format что бы посмотреть правила форматирования)";
    }
}
