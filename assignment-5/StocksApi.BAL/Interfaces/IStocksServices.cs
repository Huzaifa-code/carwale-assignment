using StocksApi.DAL.Entities;

namespace StocksApi.BAL.Interfaces
{
    public interface IStocksServices
    {
        Task<IEnumerable<StocksCars>> GetAllStocksAsync();
        Task<IEnumerable<StocksCars>> GetStocksByFilterAsync(Filters filters);

        bool IsValueForMoney(decimal price, int kms);

        Task<StocksCars> CreateStockAsync(StocksCars stock);
        Task<bool> UpdateStockAsync(StocksCars stock);
        Task<bool> DeleteStockAsync(int id);
    }
}