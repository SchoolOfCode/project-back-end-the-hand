using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Restaurant_DeleteRestaurantById
    {
        [Fact]
        public void WhenDeleteFunctionIsCalled_IfValidId_ReturnsOkStatus()
        {
            int restaurantId = 1;
            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.Delete(restaurantId));

            var controller = new RestaurantController(mockRepository.Object);

            var result = controller.Delete(restaurantId);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = viewResult.Value;

            mockRepository.Verify(repo => repo.Delete(restaurantId), Times.Once);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.Equal(model, $"Restaurant with {restaurantId} has been deleted");
        }
        
        [Fact]
        public void WhenDeleteFunctionIsCalled_IfExceptionThrown_ReturnsNotFoundStatus()
        {
            int restaurantId = 1;
            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.Delete(restaurantId)).Throws(new Exception());

            var controller = new RestaurantController(mockRepository.Object);

            var result = controller.Delete(restaurantId);

            var viewResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, viewResult.StatusCode);
        }

    }
}
