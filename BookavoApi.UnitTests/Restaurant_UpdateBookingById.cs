using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Restaurant_UpdateRestaurantById
    {
        [Fact]
        public async void WhenUpdateFunctionIsCalled_IfValidId_ReturnsOkStatusAndUpdatedRestaurant()
        {
            int restaurantId = 1;
            var testRestaurant = new Restaurant() { RestaurantName = "Oana's Test Restaurant", Capacity = 14};

            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.Update(testRestaurant)).Returns(Task.FromResult<Restaurant>(testRestaurant));;

            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.Update(restaurantId, testRestaurant);
            
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Restaurant>(viewResult.Value);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.Equal(restaurantId, model.Id);
            Assert.Equal(testRestaurant.RestaurantName, model.RestaurantName);
            Assert.Equal(testRestaurant.Capacity, model.Capacity);
        }
        
        [Fact]
        public async void WhenUpdateFunctionIsCalled_IfExceptionThrown_ReturnsBadRequestStatus()
        {
            int restaurantId = 1;
            var testRestaurant = new Restaurant() {};

            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.Update(testRestaurant)).Throws(new Exception());;

            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.Update(restaurantId, testRestaurant);

            var viewResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, viewResult.StatusCode);
        }

    }
}
