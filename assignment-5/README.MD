


### Command to add reference :  

example to connect DAL with StocksApi (Presentation Layer)
```
dotnet add .\StocksApi.DAL\ reference .\StocksApi\ 
```


My Folder Structure :

- StocksApi (Presentation Layer)
    - controller
    - Dtos
    - Mappers
- StocksApi.BAL - Business Access Layer
    - Interfaces
        - IStocksServices.cs
    - Services
        - StocksServices.cs
- StocksApi.DAL - Data Access Layer
    - Entities
    - Interfaces
        - IStockRepo.cs
    - Repositories
        - StocksRepo.cs