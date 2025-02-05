using Carwale.DAL.Interfaces;
using Dapper;
using Carwale.DAL.Data;
using Carwale.DAL.Entities;
using Carwale.DAL.Enums;


namespace Carwale.DAL.Repositories
{
    public class StocksRepo : IStocksRepo
    {   
        private readonly DatabaseContext _dbContext;


        public StocksRepo(DatabaseContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task<IEnumerable<StocksCars>> GetAllStocksAsync(int? pageNo, int? pageSize)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                // var query = "SELECT * FROM StocksCars ORDER BY Id LIMIT @PageSize OFFSET @Offset";
                var query = @"
                    SELECT 
                        sc.Id,
                        sc.RegistrationNo,
                        sc.MakeId,
                        sc.ModelId,
                        sc.FuelId,
                        sc.CityId,
                        c.CityName,
                        sc.Image,
                        m.MakeName,
                        mo.ModelName,
                        sc.Price,
                        f.FuelName as FuelType
                    FROM stockscars sc
                    JOIN cities c ON sc.CityId = c.id
                    JOIN fueltype f ON sc.FuelId = f.FuelId
                    JOIN make m ON sc.MakeId = m.id
                    JOIN model mo ON sc.ModelId = mo.id
                    ORDER BY sc.Id
                    LIMIT @PageSize OFFSET @Offset";
                
                return await connection.QueryAsync<StocksCars>(query,
                    new
                    {
                        PageSize = pageSize,
                        Offset = (pageNo - 1) * pageSize
                    }
                );
            }
            
        }

        public async Task<IEnumerable<Cities>> GetAllCitiesAsync(){
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM Cities";
                return await connection.QueryAsync<Cities>(query);
            }
        }

        public async Task<IEnumerable<Make>> GetAllMakeNamesAsync()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT Id, MakeName FROM Make";
                return await connection.QueryAsync<Make>(query);
            }
        }

        public async Task<IEnumerable<Model>> GetAllModelsByMakeIdAsync(int makeId)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT Id, MakeId, ModelName, MaxAllowedPrice, MinAllowedPrice FROM Model WHERE MakeId = @MakeId";
                return await connection.QueryAsync<Model>(query, new { MakeId = makeId });
            }
        }

        public async Task<IEnumerable<StocksCars>> GetStocksByFilterAsync(Filters filters, int? pageNo, int? pageSize)
        {   
           
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM StocksCars WHERE 1=1";

                // For FuelType=1+2 , show both Petrol and Diesel
                if (!string.IsNullOrEmpty(filters.FuelTypes))
                {
                    query += " AND FuelId IN @FuelIds";
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

                if(filters.CityId.HasValue && filters.CityId != 0){
                    query += " AND CityId = @CityId ";
                }

                query += " ORDER BY Id LIMIT @PageSize OFFSET @Offset";

                return await connection.QueryAsync<StocksCars>(query, new
                {
                    FuelIds = !string.IsNullOrWhiteSpace(filters.FuelTypes) 
                        ? filters.FuelTypes.Split('+')
                                .Where(x => !string.IsNullOrWhiteSpace(x))  
                                .Select(int.Parse)
                                .ToList()
                        : new List<int>(),
                    MinBudget = !string.IsNullOrEmpty(filters.Budget) && filters.Budget.Split('-').Length == 2 
                                ? (decimal?)decimal.Parse(filters.Budget.Split('-')[0])*100000
                                : !string.IsNullOrEmpty(filters.Budget) && filters.Budget.Split('-').Length == 1 
                                ? (decimal?)decimal.Parse(filters.Budget)*100000 
                                : null,
                    MaxBudget = !string.IsNullOrEmpty(filters.Budget) && filters.Budget.Split('-').Length == 2 
                                ? (decimal?)decimal.Parse(filters.Budget.Split('-')[1])*100000
                                : null,
                    CityId = filters?.CityId ?? (int?)null,
                    PageSize = pageSize,
                    Offset = (pageNo - 1) * pageSize
                });
            }           
        }

        public async Task<StocksCars> CreateStockAsync(StocksCars stock)
        {   
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "INSERT INTO StocksCars (MakeId, FuelId, Price, CityId, ModelId, RegistrationNo, Image) " +
                            "VALUES (@MakeId, @FuelId, @Price, @CityId, @ModelId, @RegistrationNo, @Image); " +
                            "SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(query, stock);
                stock.Id = id;
                return stock;
            }
           
        }
        
        public async Task<bool> DeleteStockAsync(int id)
        {   
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "DELETE FROM StocksCars WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
            
        }

        
    }
}