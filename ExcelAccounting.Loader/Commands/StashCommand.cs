namespace ExcelAccounting.Loader.Commands
{
    public class StashCommand : ICommand
    {
        private readonly StashModel _model;
        public StashCommand(StashModel model) => _model = model;
        public StashCommand(List<string> args)
        {
            _model = new();
            int countArgs = args.Count;
            if (countArgs < 2 || countArgs > 3)
                throw new ArgumentException(ICommand.ErrorParseMessage);

            _model.Name = args.First().Trim();
            if (countArgs == 2)
            {

            }

        }
        public void Execute(Messanger messanger)
        {
            throw new NotImplementedException();
        }
    }
    public class StashModel
    {
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        public double Rate { get; set; }

    }
}
