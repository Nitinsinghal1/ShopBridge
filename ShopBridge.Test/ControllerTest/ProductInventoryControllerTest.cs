using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.API;
using ShopBridge.Contracts.Repositories;
using ShopBridge.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Test.ControllerTest
{
    public class ProductInventoryControllerTest
    {
        private Mock<IProductInventory> _inventoryService;
        private ProductInventoryController _controller;

        public ProductInventoryControllerTest()
        {
            _inventoryService = new Mock<IProductInventory>();
            _controller = new ProductInventoryController(_inventoryService.Object);
        }

        [Fact]
        public async void InventoryDetailsList_Ok()
        {
            // Arrange
            var response = new List<InventoryDetailsList>();
            response.Add(new InventoryDetailsList()
            {
                Id = 1,
                Name = "Nitin",
            });

            _inventoryService.Setup(ser => ser.GetAllInventory()).Returns(Task.FromResult(response.AsEnumerable()));
            // Act
            var result = await _controller.GetInventries();
            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);

        }

        [Fact]
        public async void InventoryDetailsList_Error()
        {
            // Arrange

            _inventoryService.Setup(ser => ser.GetAllInventory()).Throws(new Exception("Error"));
            // Act
            var result = await _controller.GetInventries();
            // Assert
            var res = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, res.StatusCode);

        }

        [Fact]
        public async void InventoryDetail_GetInventory_Ok()
        {
            int id = 1;
            // Arrange
            SpecificInventory inventoryDetail = new SpecificInventory()
            {
                ID = 1,
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            _inventoryService.Setup(ser => ser.GetInventory(id)).Returns(Task.FromResult(inventoryDetail));
            // Act
            var result = await _controller.GetInventory(id);
            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);

        }
        [Fact]
        public async void InventoryDetail_GetInventory_NotOk()
        {
            // Arrange
            int id = 2;

            // Act
            var result = await _controller.GetInventory(id);
            // Assert
            var res = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, res.StatusCode);

        }

        [Fact]
        public async void InventoryDetail_DeleteInventory_Ok()
        {
            int id = 3;

            _inventoryService.Setup(ser => ser.DeleteInventory(id)).Returns(Task.FromResult(1));
            // Act
            var result = await _controller.DeleteInventory(id);
            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);

        }
        [Fact]
        public async void InventoryDetail_DeleteInventory_NotOk()
        {
            // Arrange
            int id = 100;
            // Act
            var result = await _controller.DeleteInventory(id);
            // Assert
            var res = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, res.StatusCode);

        }

        [Fact]
        public async void SaveInventory_Ok()
        {
            // Arrange
            InventoryDetails inventaryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventoryService.Setup(ser => ser.SaveInventory(It.IsAny<InventoryDetails>())).Returns(Task.FromResult(1));
            // Act
            var response = await _controller.SaveInventory(inventaryDetail);
            // Assert
            var res = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal("Inventory is added successfully", res.Value);
        }

        [Fact]
        public async void SaveInventory_NotOk()
        {
            // Arrange
            InventoryDetails inventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            // Act
            var response = await _controller.SaveInventory(inventoryDetail);
            // Assert
            var res = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, res.StatusCode);
            Assert.Equal("Inventory is not added successfully", res.Value);
        }

        [Fact]
        public async void SaveInventory_Error()
        {
            // Arrange
            InventoryDetails inventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
            };

            _inventoryService.Setup(ser => ser.SaveInventory(inventoryDetail)).Throws(new Exception("Error"));
            // Act
            var response = await _controller.SaveInventory(inventoryDetail);
            // Assert
            var res = Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(500, res.StatusCode);
        }

        [Fact]
        public async void UpdateInventory_Ok()
        {
            int id = 2;
            // Arrange
            InventoryDetails inventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventoryService.Setup(ser => ser.UpdateInventory(It.IsAny<InventoryDetails>(), id)).Returns(Task.FromResult(1));
            // Act
            var response = await _controller.UpdateInventory(inventoryDetail, id);
            // Assert
            var res = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal("Inventory is updated successfully", res.Value);
        }

        [Fact]
        public async void UpdateInventory_NotOk()
        {
            int id = 2;
            // Arrange
            InventoryDetails inventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            // Act
            var response = await _controller.UpdateInventory(inventoryDetail, id);
            // Assert
            var res = Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, res.StatusCode);
        }

        [Fact]
        public async void UpdateInventory_Error()
        {
            int id = 2;
            // Arrange
            InventoryDetails inventoryDetail = new InventoryDetails()
            {
                Name = "Nitin",
            };

            _inventoryService.Setup(ser => ser.UpdateInventory(inventoryDetail, id)).Throws(new Exception("Error"));
            // Act
            var response = await _controller.UpdateInventory(inventoryDetail, id);
            // Assert
            var res = Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(500, res.StatusCode);
        }

    }
}
