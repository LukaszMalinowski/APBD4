namespace cwiczenia4_zen_s19743.Exceptions
{
    public class OrderDoesntExistException : BaseException
    {
        public OrderDoesntExistException(string message) : base(message)
        {
        }
    }
}