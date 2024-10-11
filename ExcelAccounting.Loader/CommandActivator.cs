using ExcelAccounting.Loader.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelAccounting.Loader
{
    public static class CommandActivator
    {
        public static ICommand CreateCommand(string message)
        {
            ICommand command = new MessageCommand("");
            var parametrsCommand = message.Split(";");
            var commandStr = parametrsCommand.First().ToLower();
            if (commandStr.Contains("/"))
            {
                if (parametrsCommand.Length == 1) message = "";
                else message = message.Replace($"{commandStr};", "", StringComparison.CurrentCultureIgnoreCase).Trim();
            }
            var parametrs = message.Split(';').ToList();
            commandStr = commandStr.Trim();
            try
            {
                switch (commandStr)
                {
                    case "/start":
                        command = new MessageCommand(TgBot._sartText);
                        break;
                    case "/about":
                        command = new MessageCommand(TgBot._aboutText);
                        break;
                    case "/format":
                        command = new MessageCommand(TgBot._formatText);
                        break;
                    case "/help":
                        command = new MessageCommand(TgBot._helpText);

                        break;
                    case "/rate":
                        command = new RateCommand(parametrs);
                        break;
                    case "/report":
                        command = new ReportCommand(parametrs);
                        break;
                    case "/stash":
                        command = new StashCommand(parametrs);
                        break;
                    default:
                        command = new TransactionCommand(parametrs);
                        break;
                }
            }
            catch (Exception e)
            {
                command = new MessageCommand(e.Message);
            }
            return command;
        }
    }
}
