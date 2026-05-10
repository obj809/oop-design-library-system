namespace SkmProject
{
    // IComparable interface declaration - LibraryItem generic
    public abstract class LibraryItem : IComparable<LibraryItem>
    {
        private string _title = "";
        private string _itemId = "";
        private bool _isBorrowed;

        public LibraryItem(string title, string itemId)
        {
            Title = title;
            ItemId = itemId;
            _isBorrowed = false;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be empty.");
                }
                _title = value;
            }
        }

        public string ItemId
        {
            get { return _itemId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Item ID cannot be empty.");
                }
                _itemId = value;
            }
        }

        public bool IsBorrowed
        {
            get { return _isBorrowed; }
            set { _isBorrowed = value; }
        }

        public abstract int LoanPeriodDays { get; }

        public abstract void Print();

        // Interface implementation - contract method
        public int CompareTo(LibraryItem? other)
        {
            if (other == null)
            {
                return 1;
            }
            return _title.CompareTo(other._title);
        }
    }
}
