namespace SkmProject
{
    // Book inherits from the LibraryItem abstract class
    public class Book : LibraryItem
    {
        private string _author = "";

        public Book(string title, string author, string itemId) : base(title, itemId)
        {
            Author = author;
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Author cannot be empty.");
                }
                _author = value;
            }
        }

        public override int LoanPeriodDays
        {
            get { return 14; }
        }

        public override void Print()
        {
            Console.WriteLine("Book");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {_author}");
            Console.WriteLine($"Item ID: {ItemId}");
            Console.WriteLine($"Borrowed: {IsBorrowed}");
            Console.WriteLine($"Loan: {LoanPeriodDays} days");
        }
    }
}
