using ExcelAccounting.Loader.Commands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExcelAccounting.Loader
{
    public static class DataWorker
    {
        static public readonly string _transactionPath = Path.Combine("Data", "Data.txt");
        static public readonly string _stashPath = Path.Combine("Data", "Stash.txt");
        public static List<TransactionModel> TransactionModels { get; } = new();
        public static List<StashModel> StashModels { get; } = new();

        public static void LoadData()
        {
            foreach (var note in File.ReadAllLines(_transactionPath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(note)) continue;
                var parametrs = note.Split(';');
                var entity = new TransactionModel()
                {
                    Account = parametrs[0],
                    Type = parametrs[1],
                    Subtype = parametrs[2],
                    Decription = parametrs[3],
                    Date = DateOnly.Parse(parametrs[4]),
                    Value = double.Parse(parametrs[5])
                };
                TransactionModels.Add(entity);
            }
            foreach (var note in File.ReadAllLines(_stashPath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(note)) continue;
                var parametrs = note.Split(';');
                var entity = new StashModel()
                {
                    Name = parametrs[0],
                    Value = double.Parse(parametrs[1]),
                };
                if (!string.IsNullOrWhiteSpace(parametrs[2]))
                    entity.Rate = double.Parse(parametrs[2]);
                StashModels.Add(entity);
            }
        }
        public static void AddTransaction(TransactionModel model)
        {
            File.AppendAllText(_transactionPath, model.ToString());
            TransactionModels.Add(model);
        }

        public static void SaveStashs()
        {
            File.WriteAllText(_stashPath,TgBot._headerStashFileText);
            File.AppendAllLines(_stashPath,StashModels.Select(x=>x.ToString()));
        }
    }
}
