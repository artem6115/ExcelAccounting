using System.Reflection;
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
            if (!double.TryParse(args[1].Trim(), out var value))
                throw new FormatException(ICommand.ErrorParseValue);
            _model.Value = value;
            if (countArgs == 3)
            {
                if (!double.TryParse(args[2].Trim(), out var valueRate))
                    throw new FormatException(ICommand.ErrorParseValue);
                if(valueRate<0)
                    throw new ArgumentOutOfRangeException(ICommand.ErrorNegativeRate);
                _model.Rate = valueRate;
            }
        }
        public async void Execute(ITelegramBotClient bot, long chatId)
        {
            var entity = DataWorker.StashModels.FirstOrDefault(x => x == _model);
            if (entity is not null)
            {
                entity.Value += _model.Value;
                if (_model.Rate is not null)
                    entity.Rate = _model.Rate;
            }
            else
                DataWorker.StashModels.Add(_model);
            DataWorker.SaveStashs();
            await SendMessageAsync(bot, chatId, $"Добавлено на хранение в {_model.Name}");
        }


    }
    public class StashModel
    {
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        public double? Rate { get; set; }

        public override string ToString()
        {
            return $"{Name};{Value};{Rate}";
        }
        public static bool operator == (StashModel left, StashModel right)
            => string.Equals(left.Name, right.Name, StringComparison.CurrentCultureIgnoreCase);
        public static bool operator !=(StashModel left, StashModel right)
            => !string.Equals(left.Name, right.Name, StringComparison.CurrentCultureIgnoreCase);


    }
}
