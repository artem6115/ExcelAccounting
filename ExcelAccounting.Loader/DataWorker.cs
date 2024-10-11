using ExcelAccounting.Loader.Commands;

namespace ExcelAccounting.Loader
{
    public static class DataWorker
    {
        static public readonly string _transactionPath = Path.Combine("Data", "Data.txt");
        static public readonly string _stashPath = Path.Combine("Data", "Stash.txt");
        public static List<StashModel> StashModels { get; set; } = new();
        public static List<TransactionModel> TransactionModels { get; set; } = new();


        public static void StashSave()
        {
            var newStrData = StashModels.Select(x => x.ToString());
            File.WriteAllLines(_stashPath, newStrData);
        }
        public static List<TransactionModel> LoadData()
        {
            foreach(var note in File.ReadAllLines(_transactionPath).Skip(1))
            {
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
            return TransactionModels;
        }
        public static List<StashModel> LoadStash()
        {
            foreach (var note in File.ReadAllLines(_stashPath))
            {
                var parametrs = note.Split(';');
                var entity = new StashModel()
                {
                    Name = parametrs[0],
                    Value = double.Parse(parametrs[1]),
                    Rate = double.Parse(parametrs[2])
                };
                StashModels.Add(entity);
            }
            return StashModels;
        }

        public static void AddTransaction(TransactionModel model)
        {
            File.AppendAllText(_transactionPath, model.ToString());
            TransactionModels.Add(model);
        }
    }
}
