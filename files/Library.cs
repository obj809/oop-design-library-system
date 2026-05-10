namespace SkmProject
{
    // Delegate type definition
    public delegate int LibraryItemComparer(LibraryItem a, LibraryItem b);

    public class Library
    {
        private List<LibraryItem> _items;
        private List<Member> _members;
        private List<Transaction> _transactions;

        public Library()
        {
            _items = new List<LibraryItem>();
            _members = new List<Member>();
            _transactions = new List<Transaction>();
        }

        public void AddItem(LibraryItem item)
        {
            _items.Add(item);
        }

        public void AddMember(Member member)
        {
            _members.Add(member);
        }

        public LibraryItem? FindItem(string title)
        {
            foreach (LibraryItem item in _items)
            {
                if (item.Title == title)
                {
                    return item;
                }
            }
            return null;
        }

        public Member? FindMember(string id)
        {
            foreach (Member member in _members)
            {
                if (member.MemberId == id)
                {
                    return member;
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        // method that accepts a delegate parameter
        public void SortCatalogueBy(LibraryItemComparer compare)
        {
            // lambda passed as an argument, calls the compare delegate
            _items.Sort((a, b) => compare(a, b));
        }

        public void PrintCatalogue()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Catalogue:");
            if (_items.Count == 0)
            {
                Console.WriteLine("(no items)");
            }
            else
            {
                foreach (LibraryItem item in _items)
                {
                    Console.WriteLine("-----------------");
                    item.Print();
                }
            }
            Console.WriteLine("-----------------");
        }

        public void PrintMembers()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Members:");
            if (_members.Count == 0)
            {
                Console.WriteLine("(no members)");
            }
            else
            {
                foreach (Member member in _members)
                {
                    Console.WriteLine("-----------------");
                    member.Print();
                }
            }
            Console.WriteLine("-----------------");
        }

        public void PrintTransactionHistory()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Transaction history:");
            if (_transactions.Count == 0)
            {
                Console.WriteLine("(no transactions)");
            }
            else
            {
                foreach (Transaction transaction in _transactions)
                {
                    Console.WriteLine("-----------------");
                    transaction.Print();
                }
            }
            Console.WriteLine("-----------------");
        }
    }
}
