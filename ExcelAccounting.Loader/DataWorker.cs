using ExcelAccounting.Loader.Commands;

namespace ExcelAccounting.Loader
{
    public static class DataWorker
    {
        static public readonly string _transactionPath = Path.Combine("Data", "Data.txt");
        static public readonly string _stashPath = Path.Combine("Data", "Stash.txt");
        public static List<StashModel> StashModels { get; set; } = new();
        public static List<TransactionModel> TransactionModels { get; set; } = new();


        public static void StashWrite()
        {
            var newStrData = StashModels.Select(x => x.ToString());
            File.WriteAllLines(_stashPath, newStrData);
        }
        public static void TransactionWrite()
        {
            var newStrData = TransactionModels.Select(x => x.ToString());
            File.WriteAllLines(_transactionPath, newStrData);
        }
    }
}
