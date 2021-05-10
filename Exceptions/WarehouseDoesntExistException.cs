using System;

namespace cwiczenia4_zen_s19743.Exceptions
{
    public class WarehouseDoesntExistException : BaseException
    {
        public WarehouseDoesntExistException(int? idWarehouse) : base("Warehouse with id: " + idWarehouse + " doesn't exist.")
        {
        }
    }
}