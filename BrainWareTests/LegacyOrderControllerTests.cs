using Xunit;
using Moq;
using BrainWare.Models;
using BrainWare.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TestProject1
{
    public class LegacyOrderControllerTests
    {
        [Fact]
        public async Task LegacyOrderNotFound()
        {
            // Arrange
            var testId = 1;
            var mockRepo = new Mock<BrainWareContext>();
            mockRepo.Setup(x => x.Companies.FindAsync(testId))
                .ReturnsAsync((Company)null);
            var controller = new LegacyOrderController(mockRepo.Object);

            // Act
            var response = await controller.GetLegacyOrder(1);
            
            // Asssert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task LegacyOrderCalculatesTotal()
        {
            // Arrange
            var testId = 1;
            var fakeCompany = new Company
            {
                CompanyId = testId,
                Name = "FakeCo"
            };
            var order = new Order 
            {
                OrderId = 2,
                Description = "Stuff",
                CompanyId = testId,
                Company = fakeCompany
            };
            var product = new Product
            {
                ProductId = 3,
                Name = "Thing",
                Price = 5
            };
            var orderproduct = new Orderproduct
            {
                OrderId = 2,
                ProductId = 3,
                Price = 5,
                Quantity = 5,
                Product = product,
                Order = order
            };
            product.Orderproducts.Add(orderproduct);
            order.Orderproducts.Add(orderproduct);
            fakeCompany.Orders.Add(order);

            var mockRepo = new Mock<BrainWareContext>();
            mockRepo.Setup(x => x.Companies.FindAsync(testId))
                .ReturnsAsync(fakeCompany);

            var controller = new LegacyOrderController(mockRepo.Object);

            // Act
            var response = await controller.GetLegacyOrder(1);

            // Assert

            var actionResult = Assert.IsType<ActionResult<IEnumerable<LegacyOrder>>>(response);
            var returnValue = Assert.IsType<List<LegacyOrder>>(actionResult.Value);
            var legacyOrderResult = returnValue.FirstOrDefault();
            Assert.NotNull(legacyOrderResult);
            Assert.Equal((decimal)25, legacyOrderResult.OrderTotal);
        }
    }
}