namespace StocksApi.DAL.Entities
{
    public class StocksCars
    {
        public int Id { get; set; } 
        public required string MakeName { get; set; } 
        public required string FuelType { get; set; } 
        public int Kms { get; set; }
        public required decimal Price { get; set; }
        public required string Location { get; set; }
        public required string City { get; set; }
        public required int MakeYear { get; set; } 
        public required string ModelName { get; set; } 


    }
}