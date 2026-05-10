namespace SkmProject
{
    public abstract class Transaction
    {
        protected Member _member;
        protected LibraryItem _item;
        private bool _executed = false;
        private bool _reversed = false;
        private DateTime _dateStamp;

        public bool Executed
        {
            get { return _executed; }
        }

        public bool Reversed
        {
            get { return _reversed; }
        }

        public DateTime DateStamp
        {
            get { return _dateStamp; }
        }

        public abstract bool Success { get; }

        public Transaction(Member member, LibraryItem item)
        {
            _member = member;
            _item = item;
        }

        public virtual void Execute()
        {
            if (_executed)
            {
                throw new Exception("Cannot execute this transaction as it has already been completed");
            }
            if (_reversed)
            {
                throw new Exception("Cannot re-execute a reversed transaction");
            }
            _executed = true;
            _dateStamp = DateTime.Now;
        }

        public virtual void Rollback()
        {
            if (_reversed)
            {
                throw new Exception("Cannot rollback this transaction as it has already been reversed");
            }
            if (!_executed)
            {
                throw new Exception("Cannot rollback this transaction as it has not been executed");
            }
            _reversed = true;
            _executed = false;
            _dateStamp = DateTime.Now;
        }

        public abstract void Print();
    }
}
