namespace SkmProject
{
    public class Program
    {
        public static void Main()
        {
            Library library = new Library();

            MenuOption userSelection;
            do
            {
                userSelection = ReadUserOption();
                switch (userSelection)
                {
                    case MenuOption.AddItem:
                        DoAddItem(library);
                        break;

                    case MenuOption.PrintCatalogue:
                        DoPrintCatalogue(library);
                        break;

                    case MenuOption.AddMember:
                        DoAddMember(library);
                        break;

                    case MenuOption.PrintMembers:
                        DoPrintMembers(library);
                        break;

                    case MenuOption.Borrow:
                        DoBorrow(library);
                        break;

                    case MenuOption.Return:
                        DoReturn(library);
                        break;

                    case MenuOption.PrintHistory:
                        DoPrintHistory(library);
                        break;

                    case MenuOption.SortCatalogue:
                        DoSortCatalogue(library);
                        break;
                }
            } while (userSelection != MenuOption.Quit);
        }

        public static void DoAddItem(Library library)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Add item function activated");
            Console.WriteLine("Type: 1 = Book, 2 = DVD, 3 = Magazine");
            Console.Write("Enter type [1-3]: ");

            int type;
            try
            {
                type = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                type = -1;
            }

            if (type < 1 || type > 3)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM NOT ADDED: invalid type");
                Console.WriteLine("-----------------");
                return;
            }

            Console.Write("Enter title: ");
            string title = Console.ReadLine() ?? "";
            Console.Write("Enter item ID: ");
            string itemId = Console.ReadLine() ?? "";

            try
            {
                LibraryItem item;
                if (type == 1)
                {
                    Console.Write("Enter author: ");
                    string author = Console.ReadLine() ?? "";
                    item = new Book(title, author, itemId);
                }
                else if (type == 2)
                {
                    Console.Write("Enter director: ");
                    string director = Console.ReadLine() ?? "";
                    item = new DVD(title, director, itemId);
                }
                else
                {
                    Console.Write("Enter issue number: ");
                    int issueNumber = Convert.ToInt32(Console.ReadLine());
                    item = new Magazine(title, issueNumber, itemId);
                }
                library.AddItem(item);
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM ADDED");
                Console.WriteLine("-----------------");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine($"ITEM NOT ADDED: {ex.Message}");
                Console.WriteLine("-----------------");
            }
            catch (FormatException)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM NOT ADDED: issue number must be a whole number");
                Console.WriteLine("-----------------");
            }
        }

        public static void DoPrintCatalogue(Library library)
        {
            library.PrintCatalogue();
        }

        public static void DoAddMember(Library library)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Add member function activated");
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter member ID: ");
            string memberId = Console.ReadLine() ?? "";
            Console.Write("Enter max loans: ");
            int maxLoans;
            try
            {
                maxLoans = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("MEMBER NOT ADDED: Max loans must be a whole number.");
                Console.WriteLine("-----------------");
                return;
            }

            try
            {
                Member member = new Member(name, memberId, maxLoans);
                library.AddMember(member);
                Console.WriteLine("-----------------");
                Console.WriteLine("MEMBER ADDED");
                Console.WriteLine("-----------------");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine($"MEMBER NOT ADDED: {ex.Message}");
                Console.WriteLine("-----------------");
            }
        }

        public static void DoPrintMembers(Library library)
        {
            library.PrintMembers();
        }

        public static void DoBorrow(Library library)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Borrow function activated");
            Console.Write("Enter member ID: ");
            string memberId = Console.ReadLine() ?? "";
            Member? member = library.FindMember(memberId);
            if (member == null)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM NOT BORROWED: member not found");
                Console.WriteLine("-----------------");
                return;
            }

            Console.Write("Enter item title: ");
            string title = Console.ReadLine() ?? "";
            LibraryItem? item = library.FindItem(title);
            if (item == null)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM NOT BORROWED: item not found");
                Console.WriteLine("-----------------");
                return;
            }

            BorrowTransaction transaction = new BorrowTransaction(member, item);
            library.ExecuteTransaction(transaction);

            if (transaction.Success)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM BORROWED");
                Console.WriteLine("-----------------");
            }
            else
            {
                Console.WriteLine("-----------------");
                if (item.IsBorrowed)
                {
                    Console.WriteLine("ITEM NOT BORROWED: item is already borrowed");
                }
                else
                {
                    Console.WriteLine("ITEM NOT BORROWED: member is at loan limit");
                }
                Console.WriteLine("-----------------");
            }
        }

        public static void DoReturn(Library library)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Return function activated");
            Console.Write("Enter member ID: ");
            string memberId = Console.ReadLine() ?? "";
            Member? member = library.FindMember(memberId);
            if (member == null)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM NOT RETURNED: member not found");
                Console.WriteLine("-----------------");
                return;
            }

            Console.Write("Enter item title: ");
            string title = Console.ReadLine() ?? "";
            LibraryItem? item = library.FindItem(title);
            if (item == null)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM NOT RETURNED: item not found");
                Console.WriteLine("-----------------");
                return;
            }

            ReturnTransaction transaction = new ReturnTransaction(member, item);
            library.ExecuteTransaction(transaction);

            if (transaction.Success)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM RETURNED");
                Console.WriteLine("-----------------");
            }
            else
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("ITEM NOT RETURNED: item was not borrowed");
                Console.WriteLine("-----------------");
            }
        }

        public static void DoPrintHistory(Library library)
        {
            library.PrintTransactionHistory();
        }

        public static void DoSortCatalogue(Library library)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Sort catalogue");
            Console.WriteLine("1 = by title, 2 = by item ID, 3 = by availability");
            Console.Write("Enter sort key [1-3]: ");

            int key;
            try
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                key = -1;
            }

            if (key < 1 || key > 3)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("SORT CANCELLED: invalid sort key");
                Console.WriteLine("-----------------");
                return;
            }

            if (key == 1)
            {
                Console.Write("Direction (1=asc, 2=desc): ");
                bool ascending = (Console.ReadLine() != "2");
                // lambda expression passed in as an argument, implements ternary operators
                // executes contract method .CompareTo() on libaryItem
                library.SortCatalogueBy((a, b) =>
                    ascending ? a.CompareTo(b) : b.CompareTo(a));
            }
            else if (key == 2)
            {
                // lambda expression passed in as an argument
                library.SortCatalogueBy((a, b) => a.ItemId.CompareTo(b.ItemId));
            }
            else
            {
                // lambda expression passed in as an argument
                library.SortCatalogueBy((a, b) => a.IsBorrowed.CompareTo(b.IsBorrowed));
            }

            library.PrintCatalogue();
        }

        public static MenuOption ReadUserOption()
        {
            int option;
            Console.WriteLine("-----------------");
            Console.WriteLine("1 will add an item, 2 will print the catalogue, 3 will add a member, 4 will print all members, 5 will borrow an item, 6 will return an item, 7 will print transaction history, 8 will sort the catalogue, and 9 will quit");
            Console.WriteLine("-----------------");
            do
            {
                Console.WriteLine("Choose an option [1-9]:");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    option = -1;
                }
            }
            while (option < 1 || option > 9);
            return (MenuOption)(option - 1);
        }

        public enum MenuOption
        {
            AddItem,
            PrintCatalogue,
            AddMember,
            PrintMembers,
            Borrow,
            Return,
            PrintHistory,
            SortCatalogue,
            Quit
        }
    }
}
