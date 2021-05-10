namespace cwiczenia4_zen_s19743.Exceptions
{
    public class ProductDoesntExistException : BaseException
    {
        public ProductDoesntExistException(int? idProduct) : base("Product with id: " + idProduct + " doesn't exist.")
        {
        }
    }
}