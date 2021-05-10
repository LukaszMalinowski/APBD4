using System;

namespace cwiczenia4_zen_s19743.Exceptions
{
    public class OrderAlreadyCompletedException : BaseException
    {
        public OrderAlreadyCompletedException(int orderId) : base("Order with id " + orderId + " has been already completed")
        {
        }
    }
}