using ExcelAccounting.Loader;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class Program
{
    public static void Main(string[] args)
    {
        var loader = new Loader();
        var apikey = loader.LoadFileAndConfig();
        DataWorker.LoadData();
        var bot = new TelegramBotClient(apikey);
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { },
        };
        bot.StartReceiving(
            TgBot.HandleUpdateAsync,
            TgBot.HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();

    }
}
