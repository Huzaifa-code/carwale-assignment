# **Stocks API - With Layered Architecture**  

This project follows a **three-layered architecture** for better code organization and maintainability.  

## **Architecture Overview**  

### **1. Presentation Layer** (`StocksApi`)  
Handles HTTP requests and responses. This layer consists of:  
- `Controllers` – Manages API endpoints.  
- `DTOs` – Defines data transfer objects for structured API responses.  
- `Mappers` – Converts between DTOs and domain models.  

### **2. Business Layer** (`StocksApi.BAL`)  
Contains the business logic and service layer. This includes:  
- `Interfaces` – Defines contracts for services.  
- `Services` – Implements business logic.  

### **3. Data Access Layer** (`StocksApi.DAL`)  
Manages database interactions and entity definitions. This includes:  
- `Entities` – Defines database models.  
- `Interfaces` – Declares repository contracts.  
- `Repositories` – Implements data access logic.  
- `Enums` – Contains enumerations for domain-specific values.  
- `Data` – Defines database context for Entity Framework.  



# Folder Structure :

- **StocksApi** (Presentation Layer)
    - controller
    - Dtos
    - Mappers
- **StocksApi.BAL** - Business Access Layer
    - Interfaces
        - IStocksServices.cs
    - Services
        - StocksServices.cs
- **StocksApi.DAL** - Data Access Layer
    - Entities
        - Filter.cs
        - StocksCars.cs
    - Interfaces
        - IStocksRepo.cs
    - Repositories
        - StocksRepo.cs
    - Enums
        - FuelType.cs
    - Data
        - DatabaseContext.cs


# Swagger : 

![](https://i.imgur.com/fQCdGpP.png)

![](https://i.imgur.com/TDQyjKj.png)