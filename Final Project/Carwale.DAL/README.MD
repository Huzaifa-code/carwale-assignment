This is DAL - Data Access Layer


### Create Table "Cities" :
```sql
CREATE TABLE Cities (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CityName VARCHAR(255) NOT NULL
);
```


### Create Table "Make" :

```sql
CREATE TABLE Make (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    MakeName VARCHAR(50) NOT NULL
);
```

### Create Table "Model":

```sql
  CREATE TABLE Model (
      Id INT PRIMARY KEY AUTO_INCREMENT,
      ModelName VARCHAR(50) NOT NULL,
      MakeId INT NOT NULL,
      MaxAllowedPrice DECIMAL(10,2) NOT NULL,
      MinAllowedPrice DECIMAL(10,2) NOT NULL,
      FOREIGN KEY (MakeId) REFERENCES Make(Id) ON DELETE CASCADE
  );
```

### FuelType Table

```sql
CREATE TABLE FuelType (
    FuelId INT PRIMARY KEY AUTO_INCREMENT,
    FuelName VARCHAR(20) NOT NULL UNIQUE
);
```

### 

```sql
CREATE TABLE StocksCars (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    RegistrationNo VARCHAR(20) UNIQUE NOT NULL,
    CityId INT NOT NULL,
    Image VARCHAR(255) DEFAULT 'https://images.pexels.com/photos/1670457/pexels-photo-1670457.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2',
    MakeId INT NOT NULL,
    ModelId INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    FuelId INT NOT NULL,
    FOREIGN KEY (CityId) REFERENCES Cities(Id) ON DELETE CASCADE,
    FOREIGN KEY (MakeId) REFERENCES Make(Id) ON DELETE CASCADE,
    FOREIGN KEY (ModelId) REFERENCES Model(Id) ON DELETE CASCADE,
    FOREIGN KEY (FuelId) REFERENCES FuelType(FuelId) ON DELETE CASCADE
);
```

![](https://i.imgur.com/2laknJA.png)


```sql
INSERT INTO model (ModelName, MakeId, MaxAllowedPrice, MinAllowedPrice)
VALUES
('Alto', (SELECT Id FROM make WHERE MakeName = 'Suzuki'), 500000, 250000),
('Swift', (SELECT Id FROM make WHERE MakeName = 'Suzuki'), 900000, 500000),
('Baleno', (SELECT Id FROM make WHERE MakeName = 'Suzuki'), 1200000, 700000),
('Wagon R', (SELECT Id FROM make WHERE MakeName = 'Suzuki'), 800000, 400000),
('Vitara Brezza', (SELECT Id FROM make WHERE MakeName = 'Suzuki'), 1200000, 900000),
('Corolla', (SELECT Id FROM make WHERE MakeName = 'Toyota'), 2000000, 1000000),
('Camry', (SELECT Id FROM make WHERE MakeName = 'Toyota'), 3500000, 2000000),
('Innova Crysta', (SELECT Id FROM make WHERE MakeName = 'Toyota'), 2200000, 1500000),
('Fortuner', (SELECT Id FROM make WHERE MakeName = 'Toyota'), 3500000, 2500000),
('Civic', (SELECT Id FROM make WHERE MakeName = 'Honda'), 1800000, 1000000),
('Accord', (SELECT Id FROM make WHERE MakeName = 'Honda'), 3000000, 1500000),
('City', (SELECT Id FROM make WHERE MakeName = 'Honda'), 1300000, 800000),
('Mustang', (SELECT Id FROM make WHERE MakeName = 'Ford'), 5000000, 3000000),
('F-150', (SELECT Id FROM make WHERE MakeName = 'Ford'), 4500000, 2000000),
('Explorer', (SELECT Id FROM make WHERE MakeName = 'Ford'), 4000000, 2500000),
('X5', (SELECT Id FROM make WHERE MakeName = 'BMW'), 8000000, 4000000),
('3 Series', (SELECT Id FROM make WHERE MakeName = 'BMW'), 6000000, 3500000),
('M-Class', (SELECT Id FROM make WHERE MakeName = 'Mercedes-Benz'), 7000000, 3500000),
('GLC', (SELECT Id FROM make WHERE MakeName = 'Mercedes-Benz'), 5500000, 3500000),
('A-Class', (SELECT Id FROM make WHERE MakeName = 'Mercedes-Benz'), 4000000, 2500000),
('X6', (SELECT Id FROM make WHERE MakeName = 'BMW'), 8500000, 5000000);
```