using System;

namespace cwiczenia4_zen_s19743.Exceptions
{
    public class ProductDoesntExistException : Exception
    {
        public ProductDoesntExistException(int? idProduct) : base("Product with id: " + idProduct + " doesn't exist.")
        {
        }
    }
}