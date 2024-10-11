using Telegram.Bot;
using Telegram.Bot.Types.Payments;

namespace ExcelAccounting.Loader.Commands
{
    public class TransactionCommand : TgSender, ICommand
    {
        private readonly TransactionModel _model;
        public TransactionCommand(TransactionModel model) => _model = model;
        public TransactionCommand(List<string> args)
        {
            _model = new();
            int countArgs = args.Count;
            if (countArgs < 2 || countArgs > 5)
                throw new ArgumentException(ICommand.ErrorParseCommand);

            _model.Account = args[0].Trim();
            if (!double.TryParse(args.Last().Trim(), out var value))
                throw new FormatException(ICommand.ErrorParseValue);
            _model.Value = value;
            _model.Date = DateOnly.FromDateTime(DateTime.Now);
            if (args.Count > 2)
            {
                _model.Type = args[1].Trim();
                if (args.Count > 3)
                {
                    _model.Subtype = args[2].Trim();
                    if(args.Count > 4)
                        _model.Decription = args[3].Trim();
                }
            }

        }
        public async void Execute(ITelegramBotClient bot, long chatId)
        {
            DataWorker.AddTransaction(_model);
            await SendMessageAsync(bot, chatId,$"Операция на сумму {_model.Value} записана.");
        }
    }
    public class TransactionModel
    {
        public string Account { get; set; } = null!;
        public string? Decription { get; set; }
        public string? Type { get; set; }
        public string? Subtype { get; set; }
        public double Value { get; set; }
        public DateOnly Date { get; set; }
        public override string ToString()
        {
            return $"{Account};{Type};{Subtype};{Decription};{Date};{Value}\n";
        }
    }
}
