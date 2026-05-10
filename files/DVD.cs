namespace SkmProject
{
    public class DVD : LibraryItem
    {
        private string _director = "";

        public DVD(string title, string director, string itemId) : base(title, itemId)
        {
            Director = director;
        }

        public string Director
        {
            get { return _director; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Director cannot be empty.");
                }
                _director = value;
            }
        }

        public override int LoanPeriodDays
        {
            get { return 7; }
        }

        public override void Print()
        {
            Console.WriteLine("DVD");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Director: {_director}");
            Console.WriteLine($"Item ID: {ItemId}");
            Console.WriteLine($"Borrowed: {IsBorrowed}");
            Console.WriteLine($"Loan: {LoanPeriodDays} days");
        }
    }
}
