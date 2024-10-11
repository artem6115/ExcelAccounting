using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ExcelAccounting.Loader.Commands
{
    public class ReportCommand : TgSender, ICommand
    {
        private readonly ReportModel _model;
        public ReportCommand(ReportModel model) => _model = model;
        public ReportCommand(List<string> args)
        {
            _model = new();
            if (!string.IsNullOrWhiteSpace(args.First()))
            {
                if (int.TryParse(args.First().Trim(), out var monthNUmber))
                    _model.Date = new DateOnly(DateTime.Now.Year, monthNUmber, 1);
                else if (DateOnly.TryParse(args.First().Trim(), out var date))
                    _model.Date = new DateOnly(date.Year,date.Month,1);
                else
                    throw new ArgumentException(ICommand.ErrorParseDate);

            }
            else _model.Date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
        }
        public async void Execute(ITelegramBotClient bot, long chatId)
        {
            var fromDate = _model.Date;
            var toDate = _model.Date.AddMonths(1);
            var balance = DataWorker.TransactionModels
                .Where(x => x.Date < toDate)
                .Sum(x => x.Value);
            var data = DataWorker.TransactionModels.Where(x => x.Date >= fromDate && x.Date < toDate);
            var income = data.Sum(x => x.Value > 0 ? x.Value : 0);
            var unincome = data.Sum(x => x.Value < 0 ? x.Value : 0);
            var result = data.Sum(x => x.Value);
            var categories = data.GroupBy(x=>x.Type).Select(x=>(x.Key,x.Sum(y=>y.Value)));
            var accounts = data.GroupBy(x => x.Account).Select(x => (x.Key, x.Sum(y => y.Value)));
            var builder = new StringBuilder();
            builder.AppendLine($"Отчётный период: {fromDate}-{toDate}");
            builder.AppendLine($"----------------------------------\n");
            builder.AppendLine($"Заработано = {income}");
            builder.AppendLine($"Потрачено = {unincome}");
            builder.AppendLine($"Разница = {result}");
            builder.AppendLine($"Баланс да отчётный месяц = {balance}");
            builder.AppendLine($"\nПо счетам: ");
            foreach (var a in accounts)
                builder.AppendLine($"Счёт - {a.Key}, сумма операций = {a.Item2}");
            builder.AppendLine($"\nПо категориям:");
            foreach (var c in categories)
                builder.AppendLine($"Категория - {c.Key}, сумма операций = {c.Item2}");
            builder.AppendLine($"\nНа текущий момент на сохранении: ");
            foreach (var s in DataWorker.StashModels)
                builder.AppendLine($"Хранилище - {s.Name}, сумма = {s.Value}");
            await SendMessageAsync(bot,chatId,builder.ToString());

        }
    }
   
    public class ReportModel
    {
        public DateOnly Date { get; set; }
    }
}
