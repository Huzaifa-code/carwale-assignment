using StocksApi.BAL.Interfaces;
using StocksApi.DAL.Entities;
using StocksApi.DAL.Interfaces;

namespace StocksApi.BAL.Services
{
    public class StocksServices : IStocksServices
    {   
        private readonly IStocksRepo _stockRepo;
        
        public StocksServices(IStocksRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IEnumerable<StocksCars>> GetAllStocksAsync()
        {
            // Call the GetAllStocksAsync method from StocksRepo
            return await _stockRepo.GetAllStocksAsync();
        }

        public async Task<IEnumerable<StocksCars>> GetStocksByFilterAsync(Filters filters)
        {
            // Call the GetStocksByFilterAsync method from StocksRepo
            return await _stockRepo.GetStocksByFilterAsync(filters);
        }

        public bool IsValueForMoney(decimal price, int kms){
            if( price < 200000 && kms < 10000 )
                return true;
            
            return false;
        }

        public async Task<StocksCars> CreateStockAsync(StocksCars stock)
        {
            return await _stockRepo.CreateStockAsync(stock); 
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            return await _stockRepo.DeleteStockAsync(id);
        }

        public async Task<bool> UpdateStockAsync(StocksCars stock)
        {
            return await _stockRepo.UpdateStockAsync(stock);
        }
    }
}