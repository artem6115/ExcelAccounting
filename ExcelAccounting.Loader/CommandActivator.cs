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
            var commandStr = message.Split(";").First().Trim().ToLower();
            if (commandStr.Contains("@"))
                message = message.Replace($"{commandStr};", "").Trim();
            var parametrs = message.Split(';').ToList();
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

                        break;
                    case "/report":

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
                Console.WriteLine(e.Message);
                command = new MessageCommand(e.Message);
            }
            return command;
        }
    }
}
