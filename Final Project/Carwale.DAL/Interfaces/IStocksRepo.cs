using Carwale.DAL.Entities;

namespace Carwale.DAL.Interfaces
{
    public interface IStocksRepo
    {
        Task<IEnumerable<StocksCars>> GetAllStocksAsync(int? pageNo, int? pageSize);
        Task<IEnumerable<Cities>> GetAllCitiesAsync();
        Task<IEnumerable<Make>> GetAllMakeNamesAsync();
        Task<IEnumerable<Model>> GetAllModelsByMakeIdAsync(int makeId);
        Task<IEnumerable<StocksCars>> GetStocksByFilterAsync(Filters filters,int? pageNo, int? pageSize);

        Task<StocksCars> CreateStockAsync(StocksCars stock);
        Task<bool> DeleteStockAsync(int id);
    }
}