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

        public int RegisterProductAtWarehouse()
        {
            throw new System.NotImplementedException();
        }
    }
}