using StocksApi.DAL.Interfaces;
using Dapper;
using StocksApi.DAL.Data;
using StocksApi.DAL.Entities;
using StocksApi.DAL.Enums;


namespace StocksApi.DAL.Repositories
{
    public class StocksRepo : IStocksRepo
    {   
        private readonly DatabaseContext _dbContext;


        public StocksRepo(DatabaseContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task<IEnumerable<StocksCars>> GetAllStocksAsync()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM CarsStocks";
                return await connection.QueryAsync<StocksCars>(query);
            }
            
        }

        public async Task<IEnumerable<StocksCars>> GetStocksByFilterAsync(Filters filters)
        {   
           
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM CarsStocks WHERE 1=1";

                // For FuelType=1+2 , show both Petrol and Diesel
                if (!string.IsNullOrEmpty(filters.FuelTypes))
                {
                    query += " AND FuelType IN @FuelTypeNames";
                }

            
                if (!string.IsNullOrEmpty(filters.Budget))
                {
                
                    var budgetRange = filters.Budget.Split('-');
                    if (budgetRange.Length == 2)
                    {
                        query += " AND Price BETWEEN @MinBudget AND @MaxBudget";
                    }else {
                        query += " AND Price >= @MinBudget";
                    }
                    
                }


                return await connection.QueryAsync<StocksCars>(query, new
                {
                    FuelTypeNames = filters.FuelTypes?.Split('+')
                                    .Select(int.Parse)
                                    .Select(value => Enum.GetName(typeof(FuelType), value))
                                    .ToList(),
                    MinBudget = !string.IsNullOrEmpty(filters.Budget) && filters.Budget.Split('-').Length == 2 
                                ? (decimal?)decimal.Parse(filters.Budget.Split('-')[0])*100000
                                : !string.IsNullOrEmpty(filters.Budget) && filters.Budget.Split('-').Length == 1 
                                ? (decimal?)decimal.Parse(filters.Budget)*100000 
                                : null,
                    MaxBudget = !string.IsNullOrEmpty(filters.Budget) && filters.Budget.Split('-').Length == 2 
                                ? (decimal?)decimal.Parse(filters.Budget.Split('-')[1])*100000
                                : null
                });
            }           
        }

        public async Task<StocksCars> CreateStockAsync(StocksCars stock)
        {   
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "INSERT INTO CarsStocks (MakeName, FuelType, Kms, Price, Location, City, MakeYear, ModelName) " +
                            "VALUES (@MakeName, @FuelType, @Kms, @Price, @Location, @City, @MakeYear, @ModelName); " +
                            "SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(query, stock);
                stock.Id = id;
                return stock;
            }
           
        }

        public async Task<bool> UpdateStockAsync(StocksCars stock)
        {   
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "UPDATE CarsStocks SET MakeName = @MakeName, FuelType = @FuelType, Kms = @Kms, " +
                            "Price = @Price, Location = @Location, City = @City, MakeYear = @MakeYear, ModelName = @ModelName " +
                            "WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(query, stock);
                return affectedRows > 0;
            }
            
        }

        public async Task<bool> DeleteStockAsync(int id)
        {   
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "DELETE FROM CarsStocks WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
            
        }
    
    }
}