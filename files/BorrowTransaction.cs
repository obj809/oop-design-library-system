namespace SkmProject
{
    public class BorrowTransaction : Transaction
    {
        private bool _success = false;

        public override bool Success
        {
            get { return _success; }
        }

        public BorrowTransaction(Member member, LibraryItem item) : base(member, item)
        {
        }

        public override void Execute()
        {
            base.Execute();
            if (_item.IsBorrowed || _member.AtLoanLimit)
            {
                _success = false;
            }
            else
            {
                _item.IsBorrowed = true;
                _member.AddLoan(_item);
                _success = true;
            }
        }

        public override void Rollback()
        {
            if (Reversed)
            {
                throw new Exception("Cannot rollback this transaction as it has already been reversed");
            }
            if (!Executed || !_success)
            {
                throw new Exception("Cannot rollback this transaction as it was not successfully executed");
            }
            _item.IsBorrowed = false;
            _member.RemoveLoan(_item);
            _success = false;
            base.Rollback();
        }

        public override void Print()
        {
            Console.WriteLine($"Borrow - Member: {_member.MemberId}, Item: {_item.Title}, Success: {_success}, Reversed: {Reversed} on {DateStamp}");
        }
    }
}
