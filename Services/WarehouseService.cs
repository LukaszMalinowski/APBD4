using cwiczenia4_zen_s19743.Exceptions;
using cwiczenia4_zen_s19743.Models.DTOs;
using cwiczenia4_zen_s19743.Repositories;

namespace cwiczenia4_zen_s19743.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }

        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto)
        {
            if(!_repository.ProductExists(warehouseDto.IdProduct))
            {
                throw new ProductDoesntExistException(warehouseDto.IdProduct);
            }
            
            if(!_repository.WarehouseExists(warehouseDto.IdWarehouse))
            {
                throw new WarehouseDoesntExistException(warehouseDto.IdWarehouse);
            }


            return 0;
        }
    }
}