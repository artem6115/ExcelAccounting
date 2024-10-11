using Telegram.Bot;

namespace ExcelAccounting.Loader.Commands
{
    public class StashCommand : TgSender, ICommand
    {
        private readonly StashModel _model;
        public StashCommand(StashModel model) => _model = model;
        public StashCommand(List<string> args)
        {
            _model = new();
            int countArgs = args.Count;
            if (countArgs < 2 || countArgs > 3)
                throw new ArgumentException(ICommand.ErrorParseCommand);

            _model.Name = args[0].Trim();
            _model.Value = double.Parse(args[1].Trim());

            if (countArgs == 3)
                _model.Rate = double.Parse(args[1].Trim());
        }
        public void Execute(ITelegramBotClient bot, long chatId)
        {

        }


    }
    public class StashModel
    {
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        public double? Rate { get; set; }

        public override string ToString()
        {
            return $"{Name};{Value};{Rate}\n";
        }

    }
}
