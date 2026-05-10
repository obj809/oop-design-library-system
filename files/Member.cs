namespace SkmProject
{
    public class Member
    {
        private string _name = "";
        private string _memberId = "";
        private int _maxLoans;
        private List<LibraryItem> _borrowedItems = new List<LibraryItem>();

        public Member(string name, string memberId, int maxLoans)
        {
            Name = name;
            MemberId = memberId;
            MaxLoans = maxLoans;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty.");
                }
                _name = value;
            }
        }

        public string MemberId
        {
            get { return _memberId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Member ID cannot be empty.");
                }
                _memberId = value;
            }
        }

        public int MaxLoans
        {
            get { return _maxLoans; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Max loans must be greater than zero.");
                }
                _maxLoans = value;
            }
        }

        public bool AtLoanLimit
        {
            get { return _borrowedItems.Count >= _maxLoans; }
        }

        public void AddLoan(LibraryItem item)
        {
            _borrowedItems.Add(item);
        }

        public void RemoveLoan(LibraryItem item)
        {
            _borrowedItems.Remove(item);
        }

        public void Print()
        {
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine($"Member ID: {_memberId}");
            Console.WriteLine($"Max loans: {_maxLoans}");
            Console.WriteLine($"Borrowed: {_borrowedItems.Count}");
        }
    }
}
