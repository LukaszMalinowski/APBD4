using System;

namespace cwiczenia4_zen_s19743.Exceptions
{
    public class OrderCreatedAtException : BaseException
    {
        public OrderCreatedAtException(DateTime? createdAt) : base("Order create date is newer than " + createdAt)
        {
        }
    }
}