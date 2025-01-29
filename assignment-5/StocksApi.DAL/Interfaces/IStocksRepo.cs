using StocksApi.DAL.Entities;

namespace StocksApi.DAL.Interfaces
{
    public interface IStocksRepo
    {
        Task<IEnumerable<StocksCars>> GetAllStocksAsync();
        Task<IEnumerable<StocksCars>> GetStocksByFilterAsync(Filters filters);

        Task<StocksCars> CreateStockAsync(StocksCars stock);
        Task<bool> UpdateStockAsync(StocksCars stock);
        Task<bool> DeleteStockAsync(int id);
    }
}