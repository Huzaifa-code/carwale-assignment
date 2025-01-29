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
            try
            {
                using (var connection = _dbContext.CreateConnection())
                {
                    var query = "SELECT * FROM CarsStocks";
                    return await connection.QueryAsync<StocksCars>(query);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching all stocks: {ex.Message}");
                throw new Exception("An error occurred while fetching stocks.", ex);
            }
            
        }

        public async Task<IEnumerable<StocksCars>> GetStocksByFilterAsync(Filters filters)
        {   
            try
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
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching stocks by filter: {ex.Message}");
                throw new Exception("An error occurred while filtering stocks.", ex);
            }
           
        }

        public async Task<StocksCars> CreateStockAsync(StocksCars stock)
        {   
            try
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
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating stock: {ex.Message}");
                throw new Exception("An error occurred while creating the stock.", ex);
            }
           
        }

        public async Task<bool> UpdateStockAsync(StocksCars stock)
        {   
            try
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
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating stock: {ex.Message}");
                throw new Exception("An error occurred while updating the stock.", ex);
            }
            
        }

        public async Task<bool> DeleteStockAsync(int id)
        {   
            try
            {
                using (var connection = _dbContext.CreateConnection())
                {
                    var query = "DELETE FROM CarsStocks WHERE Id = @Id";
                    var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting stock: {ex.Message}");
                throw new Exception("An error occurred while deleting the stock.", ex);
            }
            
        }
    
    }
}