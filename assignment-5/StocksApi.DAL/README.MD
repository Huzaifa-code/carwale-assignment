This is DAL - Data Access Layer


```sql
CREATE TABLE CarsStocks (
    Id INT AUTO_INCREMENT PRIMARY KEY,  
    Name VARCHAR(100) NOT NULL,         
    BrandName VARCHAR(100) NOT NULL,    
    FuelType VARCHAR(50) NOT NULL,     
    Kms INT NOT NULL,                   
    Price DECIMAL(18, 2) NOT NULL,   -- on road price (onwards)
    Location VARCHAR(150) NOT NULL,     
    City VARCHAR(100) NOT NULL
);
```


Add this in StocksApi.DAL.csproj to add reference to MySql.Data package which is inside StocksApi 

```cs
  <ItemGroup>
    <PackageReference Include="MySql.Data" Version="9.2.0" />
    <PackageReference Include="Dapper" Version="2.1.35" />
  </ItemGroup>
```