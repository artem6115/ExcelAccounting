using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ExcelAccounting.Loader.Commands
{
    public class RateCommand : TgSender, ICommand
    {
        private readonly RateModel _model;
        public RateCommand(RateModel model) => _model = model;
        public RateCommand(List<string> args)
        {
            _model = new();
            if (!string.IsNullOrWhiteSpace(args.First()))
                _model.Name = args.First().Trim();
        }
        public async void Execute(ITelegramBotClient bot, long chatId)
        {
            if (_model.Name is not null)
            {
                StashModel? entity = DataWorker.StashModels.FirstOrDefault(
                    x => (string.Equals(x.Name, _model.Name, StringComparison.CurrentCultureIgnoreCase))
                    );
                if (entity is null)
                {
                    await SendMessageAsync(bot, chatId, ICommand.ErrorStashNotFound);
                    return;
                }
                if (!entity.Rate.HasValue)
                {
                    await SendMessageAsync(bot, chatId, ICommand.ErrorStashHasNotRate);
                    return;
                }
               
                entity.Value *= (entity.Rate.Value / 100) + 1;
                await SendMessageAsync(bot, chatId, $"Начислены проценты на {_model.Name} = {entity.Value}");
            }
            else
            {
                var messageBuilder = new StringBuilder("Начисление прошло по счетам:\n");
                foreach(var stash in DataWorker.StashModels)
                {
                    if (stash.Rate is null) continue;
                    stash.Value *= (stash.Rate.Value / 100) + 1;
                    messageBuilder.AppendLine($"{stash.Name} = {stash.Value}");

                }
                await SendMessageAsync(bot, chatId, messageBuilder.ToString());
            }
                
            DataWorker.SaveStashs();

        }
    }
    public class RateModel
    {
        public string? Name { get; set; }
    }
}
