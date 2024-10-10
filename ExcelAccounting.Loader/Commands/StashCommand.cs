﻿namespace ExcelAccounting.Loader.Commands
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

            _model.Name = args[0].Trim();
            _model.Value = double.Parse(args[1].Trim());

            if (countArgs == 3)
                _model.Rate = double.Parse(args[1].Trim());
        }
        public void Execute(Messanger messanger)
        {
            LoadNote();

        }

        public void LoadNote()
        {
            foreach (var line in File.ReadLines(DataWorker._stashPath))
            {
                if (string.Equals
                    (_model.Name, line.Split(';')[0]
                    , StringComparison.CurrentCultureIgnoreCase))
                {

                }
            }
        }
    }
    public class StashModel
    {
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        public double? Rate { get; set; }

    }
}
