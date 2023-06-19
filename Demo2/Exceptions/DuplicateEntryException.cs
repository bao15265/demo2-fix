namespace Demo2.Exceptions
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message) : base(message)
        {
        }
    }

}
