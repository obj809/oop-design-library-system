namespace SkmProject
{
    public class Magazine : LibraryItem
    {
        private int _issueNumber;

        public Magazine(string title, int issueNumber, string itemId) : base(title, itemId)
        {
            IssueNumber = issueNumber;
        }

        public int IssueNumber
        {
            get { return _issueNumber; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Issue number must be greater than zero.");
                }
                _issueNumber = value;
            }
        }

        public override int LoanPeriodDays
        {
            get { return 3; }
        }

        public override void Print()
        {
            Console.WriteLine("Magazine");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Issue: #{_issueNumber}");
            Console.WriteLine($"Item ID: {ItemId}");
            Console.WriteLine($"Borrowed: {IsBorrowed}");
            Console.WriteLine($"Loan: {LoanPeriodDays} days");
        }
    }
}
