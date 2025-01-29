using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StocksApi.BAL.Interfaces;
using StocksApi.BAL.Services;
using StocksApi.DAL.Entities;
using StocksApi.DAL.Repositories;
using StocksApi.DAL.Interfaces;
using Moq;
using Xunit;

namespace StocksApi.Tests.Services.Tests
{
    public class StocksServicesTests
    {
        private readonly Mock<IStocksRepo> _mockStocksRepo;
        private readonly StocksServices _stocksServices;

        public StocksServicesTests()
        {
            _mockStocksRepo = new Mock<IStocksRepo>();
            _stocksServices = new StocksServices(_mockStocksRepo.Object);
        }

        [Fact]
        public async Task GetAllStocksAsync_ShouldReturnAllStocks()
        {
            // Arrange
            var mockStocks = new List<StocksCars>
            {
                new StocksCars
                {
                    Id = 1,
                    MakeName = "Toyota",
                    FuelType = "Petrol",
                    Kms = 15000,
                    Price = 500000,
                    Location = "Mumbai",
                    City = "Mumbai",
                    MakeYear = 2020,
                    ModelName = "Corolla"
                },
                new StocksCars
                {
                    Id = 2,
                    MakeName = "Honda",
                    FuelType = "Diesel",
                    Kms = 20000,
                    Price = 600000,
                    Location = "Delhi",
                    City = "Delhi",
                    MakeYear = 2019,
                    ModelName = "Civic"
                }
            };

            _mockStocksRepo.Setup(repo => repo.GetAllStocksAsync()).ReturnsAsync(mockStocks);

            // Act
            var result = await _stocksServices.GetAllStocksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockStocksRepo.Verify(repo => repo.GetAllStocksAsync(), Times.Once);
        }

        [Fact]
        public async Task GetStocksByFilterAsync_ShouldReturnFilteredStocks()
        {
            // Arrange
            var filters = new Filters { FuelTypes="1+2", Budget="1-10" };
            var mockStocks = new List<StocksCars>
            {
                new StocksCars
                {
                    Id = 1,
                    MakeName = "Toyota",
                    FuelType = "Petrol",
                    Kms = 15000,
                    Price = 500000,
                    Location = "Mumbai",
                    City = "Mumbai",
                    MakeYear = 2020,
                    ModelName = "Corolla"
                },
                new StocksCars
                {
                    Id = 2,
                    MakeName = "Honda",
                    FuelType = "Petrol",
                    Kms = 20000,
                    Price = 600000,
                    Location = "Delhi",
                    City = "Delhi",
                    MakeYear = 2019,
                    ModelName = "Civic"
                }
            };

            _mockStocksRepo.Setup(repo => repo.GetStocksByFilterAsync(filters)).ReturnsAsync(mockStocks);

            // Act
            var result = await _stocksServices.GetStocksByFilterAsync(filters);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockStocksRepo.Verify(repo => repo.GetStocksByFilterAsync(filters), Times.Once);
        }

        [Fact]
        public void IsValueForMoney_ShouldReturnTrue_WhenPriceAndKmsAreLow()
        {
            // Arrange
            decimal price = 150000;
            int kms = 5000;

            // Act
            var result = _stocksServices.IsValueForMoney(price, kms);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValueForMoney_ShouldReturnFalse_WhenPriceOrKmsAreHigh()
        {
            // Arrange
            decimal price = 250000;
            int kms = 15000;

            // Act
            var result = _stocksServices.IsValueForMoney(price, kms);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CreateStockAsync_ShouldReturnCreatedStock()
        {
            // Arrange
            var newStock = new StocksCars
            {
                MakeName = "Toyota",
                FuelType = "Petrol",
                Kms = 15000,
                Price = 500000,
                Location = "Mumbai",
                City = "Mumbai",
                MakeYear = 2020,
                ModelName = "Corolla"
            };

            var createdStock = new StocksCars
            {
                Id = 1,
                MakeName = "Toyota",
                FuelType = "Petrol",
                Kms = 15000,
                Price = 500000,
                Location = "Mumbai",
                City = "Mumbai",
                MakeYear = 2020,
                ModelName = "Corolla"
            };

            _mockStocksRepo.Setup(repo => repo.CreateStockAsync(newStock)).ReturnsAsync(createdStock);

            // Act
            var result = await _stocksServices.CreateStockAsync(newStock);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _mockStocksRepo.Verify(repo => repo.CreateStockAsync(newStock), Times.Once);
        }

        [Fact]
        public async Task UpdateStockAsync_ShouldReturnTrue_WhenStockExists()
        {
            // Arrange
            var stockToUpdate = new StocksCars
            {
                Id = 1,
                MakeName = "Toyota",
                FuelType = "Petrol",
                Kms = 15000,
                Price = 500000,
                Location = "Mumbai",
                City = "Mumbai",
                MakeYear = 2020,
                ModelName = "Corolla"
            };

            _mockStocksRepo.Setup(repo => repo.UpdateStockAsync(stockToUpdate)).ReturnsAsync(true);

            // Act
            var result = await _stocksServices.UpdateStockAsync(stockToUpdate);

            // Assert
            Assert.True(result);
            _mockStocksRepo.Verify(repo => repo.UpdateStockAsync(stockToUpdate), Times.Once);
        }

        [Fact]
        public async Task DeleteStockAsync_ShouldReturnTrue_WhenStockExists()
        {
            // Arrange
            int stockId = 1;

            _mockStocksRepo.Setup(repo => repo.DeleteStockAsync(stockId)).ReturnsAsync(true);

            // Act
            var result = await _stocksServices.DeleteStockAsync(stockId);

            // Assert
            Assert.True(result);
            _mockStocksRepo.Verify(repo => repo.DeleteStockAsync(stockId), Times.Once);
        }

        [Fact]
        public async Task DeleteStockAsync_ShouldReturnFalse_WhenStockDoesNotExist()
        {
            // Arrange
            int stockId = 999; 

            _mockStocksRepo.Setup(repo => repo.DeleteStockAsync(stockId)).ReturnsAsync(false);

            // Act
            var result = await _stocksServices.DeleteStockAsync(stockId);

            // Assert
            Assert.False(result);
            _mockStocksRepo.Verify(repo => repo.DeleteStockAsync(stockId), Times.Once);
        }
    }
}