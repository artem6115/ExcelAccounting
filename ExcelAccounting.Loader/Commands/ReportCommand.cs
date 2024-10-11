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
            if (string.IsNullOrWhiteSpace(args.First()))
            {
                if (int.TryParse(args.First().Trim(), out var monthNUmber))
                    _model.Date = new DateOnly(DateTime.Now.Year, monthNUmber, 1);
                else if (DateOnly.TryParse(args.First().Trim(), out var date))
                    _model.Date = date;
                else
                    throw new ArgumentException(ICommand.ErrorParseDate);

            }
            else _model.Date = new DateOnly(DateTime.Now.Year,DateTime.Now.Month,1);
        }
        public void Execute(ITelegramBotClient bot, long chatId)
        {
            
        }
    }
    public class ReportModel
    {
        public DateOnly? Date { get; set; }
    }
}
