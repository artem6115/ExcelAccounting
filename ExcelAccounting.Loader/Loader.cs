using System.Text;

namespace ExcelAccounting.Loader
{
    public class Loader
    {
        public string LoadFileAndConfig()
        {
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
                throw new ArgumentException("Передайте в файл Data/Apikey.txt ключ от телеграм бота");
            return api;

        }
        private IEnumerable<(string, string)> GetFiles()
        {
            yield return (Path.Combine("Data", "Data.txt"), "Счёт;Категория;Подкатегория;Дата;Описание;Сумма");
            yield return (Path.Combine("Data", "Stash.txt"), "На что копить;Отложеная сумма");
            yield return (Path.Combine("Data", "Apikey.txt"), "");
        }
        public void LoadData()
        {
            foreach (var note in File.ReadAllLines(DataWorker._transactionPath))
            {

            }
        }

    }
}
