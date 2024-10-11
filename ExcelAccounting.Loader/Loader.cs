using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExcelAccounting.Loader
{
    public class Loader
    {
        public string LoadFileAndConfig()
        {
            if(!Directory.Exists("Data"))
                Directory.CreateDirectory("Data");
            foreach (var item in GetFiles())
            {
                if (!File.Exists(item.Item1))
                {
                    using (var file = File.Create(item.Item1))
                    {
                        var message = Encoding.UTF8.GetBytes(item.Item2);
                        file.Write(message, 0, message.Length);
                    }
                }

            }
            string? api = File.ReadAllText(Path.Combine("Data", "Apikey.txt"));
            if (string.IsNullOrWhiteSpace(api))
            {
                Console.WriteLine("Передайте в файл Data / Apikey.txt ключ от телеграм бота");
                Console.ReadLine();
                throw new ArgumentException("Передайте в файл Data/Apikey.txt ключ от телеграм бота");
            }
            return api;

        }
        private IEnumerable<(string, string)> GetFiles()
        {
            yield return (Path.Combine("Data", "Data.txt"), "Счёт;Категория;Подкатегория;Описание;Дата;Сумма\n");
            yield return (Path.Combine("Data", "Stash.txt"), TgBot._headerStashFileText);
            yield return (Path.Combine("Data", "Apikey.txt"), "");
        }

    }
}
