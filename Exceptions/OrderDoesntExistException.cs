using System;

namespace cwiczenia4_zen_s19743.Exceptions
{
    public class OrderDoesntExistException : Exception
    {
        public OrderDoesntExistException(string message) : base(message)
        {
        }
    }
}