namespace Carwale.DAL.Entities
{
    public class StocksCars
    {
        public int Id { get; set; }  
        public required string RegistrationNo { get; set; } 
        public required string Image { get; set; }  
        public required int MakeId { get; set; }  
        public required int ModelId { get; set; } 
        public required int FuelId { get; set; }  
        public required decimal Price { get; set; }  
        public required int CityId { get; set; }  
         public string CityName { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string FuelType { get; set; }
    }
}
