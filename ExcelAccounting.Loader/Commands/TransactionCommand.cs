using Telegram.Bot;

namespace ExcelAccounting.Loader.Commands
{
    public class TransactionCommand : ICommand
    {
        private readonly TransactionModel _model;
        public TransactionCommand(TransactionModel model) => _model = model;
        public TransactionCommand(List<string> args)
        {
            _model = new();
            int countArgs = args.Count;
            if (countArgs < 2 || countArgs == 4 || countArgs > 5)
                throw new ArgumentException(ICommand.ErrorParseMessage);

            _model.Account = args[0].Trim();
            _model.Value = double.Parse(args.Last().Trim());
            _model.Date = DateTime.Now;
            if (args.Count > 2)
            {
                _model.Type = args[1].Trim();
                if (args.Count == 5)
                {
                    _model.Subtype = args[2].Trim();
                    _model.Decription = args[3].Trim();
                }
            }

        }
        public void Execute(ITelegramBotClient bot, long chatId)
        {
            DataWorker.TransactionModels.Add(_model);
            DataWorker.TransactionSave();
        }
    }
    public class TransactionModel
    {
        public string Account { get; set; } = null!;
        public string? Decription { get; set; }
        public string? Type { get; set; }
        public string? Subtype { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public override string ToString()
        {
            return $"{Account};{Type};{Subtype};{Decription};{Date};{Value}\n";
        }
    }
}
