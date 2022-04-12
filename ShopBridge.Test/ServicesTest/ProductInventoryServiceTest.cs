using Moq;
using ShopBridge.Contracts.Repositories;
using ShopBridge.Contracts.Services;
using ShopBridge.Data.Entity;
using ShopBridge.Data.Repositories;
using ShopBridge.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Test.ServicesTest
{
    public class ProductInventoryServiceTest
    {
        private Mock<IProductInventoryManagement> _inventoryRepository;
        private ProductInventoryService _inventoryService;

        public ProductInventoryServiceTest()
        {
            _inventoryRepository = new Mock<IProductInventoryManagement>();
            _inventoryService = new ProductInventoryService(_inventoryRepository.Object);
        }

        [Fact]
        public async void SaveInventory_Ok()
        {
            //Arrange
            InventoryDetails InventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventoryRepository.Setup(repo => repo.SaveInventory(It.IsAny<Inventory>())).Returns(Task.FromResult(1));
            //Act
            var response = await _inventoryService.SaveInventory(InventoryDetail);
            //Assert
            Assert.Equal(1, response);
        }
        [Fact]
        public async void SaveInventory_NotOk()
        {
            //Arrange
            InventoryDetails InventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            //Act
            var response = await _inventoryService.SaveInventory(InventoryDetail);
            //Assert
            Assert.Equal(0, response);
        }

        [Fact]
        public async void UpdateInventory_Ok()
        {
            int id = 1;
            //Arrange
            InventoryDetails InventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventoryRepository.Setup(repo => repo.UpdateInventory(It.IsAny<Inventory>(), id)).Returns(Task.FromResult(1));
            //Act
            var response = await _inventoryService.UpdateInventory(InventoryDetail, id);
            //Assert
            Assert.Equal(1, response);
        }
        [Fact]
        public async void UpdateInventary_NotOk()
        {
            int id = 2;
            //Arrange
            InventoryDetails InventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            //Act
            var response = await _inventoryService.UpdateInventory(InventoryDetail, id);
            //Assert
            Assert.Equal(0, response);
        }

        [Fact]
        public async void DeleteInventory_Ok()
        {
            int id = 3;
            //Arrange

            _inventoryRepository.Setup(repo => repo.DeleteInventory(id)).Returns(Task.FromResult(1));
            //Act
            var response = await _inventoryService.DeleteInventory(id);
            //Assert
            Assert.Equal(1, response);
        }
        [Fact]
        public async void DeleteInventory_NotOk()
        {
            //Arrange
            int id = 2;
            //Act
            var response = await _inventoryService.DeleteInventory(id);
            //Assert
            Assert.Equal(0, response);
        }

        [Fact]
        public async void GetInventory_Ok()
        {
            //Arrange
            int id = 1;

            Inventory inventory = new Inventory()
            {
                Id = 1,
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                TotalPrice = 10
            };
            _inventoryRepository.Setup(repo => repo.GetInventory(id)).Returns(Task.FromResult(inventory));
            //Act
            var response = await _inventoryService.GetInventory(id);
            //Assert
            var res = Assert.IsType<SpecificInventory>(response);
            Assert.Equal(inventory.Name, res.Name);
            Assert.Equal(inventory.Description, res.Description);
            Assert.Equal(inventory.PricePerUnit, res.PricePerUnit);
            Assert.Equal(inventory.Quantity, res.Quantity);
        }

        [Fact]
        public async void GetInventory_NotOk()
        {
            //Arrange
            int id = 1;
            //Act
            var response = await _inventoryService.GetInventory(id);
            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void GetAllInventory_Ok()
        {
            //Arrange
            var Inventory = new List<Inventory>();
            Inventory.Add(new Inventory()
            {
                Id = 1,
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                TotalPrice = 10
            });

            _inventoryRepository.Setup(repo => repo.GetAllInventory()).Returns(Task.FromResult(Inventory.AsEnumerable()));
            //Act
            var response = await _inventoryService.GetAllInventory();
            //Assert
            var res = Assert.IsType<List<InventoryDetailsList>>(response);
            Assert.Equal(Inventory[0].Name, res[0].Name);
        }

        [Fact]
        public async void GetAllInventory_NotOk()
        {
            //Act
            var response = await _inventoryService.GetAllInventory();
            //Assert
            var res = Assert.IsType<List<InventoryDetailsList>>(response);
            Assert.Equal(0, res.Count);
        }

    }
}
