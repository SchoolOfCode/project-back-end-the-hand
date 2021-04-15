using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Restaurant_GetRestaurantById
    {
        [Fact]
        public async Task WhenGetFunctionIsCalled_IfValidId_ReturnsOKStatusAndRestaurantWithThatId(){
            var restaurantList = new List<Restaurant>();
            restaurantList.Add(new Restaurant()
            {
                Id = 1,
                RestaurantName = "Test restaurant 1",
                Description = "Test restaurant 1 description",
                OpeningTimes = "1700",
                ClosingTimes  = "1800",
                PhoneNumber = "01219999999",
                AddressLine1 = "Test restaurant 1 address",
                Area ="Test restaurant1 area",
                Postcode = "Test restaurant1 postcode",
                WebsiteURL = "www.test-restaurant1.co.uk",
                PhotoURL = "wwww.test-restaurant1.jpg",
                AdditionalInfo = "Test restaurant1 additional info",
                Cuisine = "Italy",
                Capacity = 5,
                RestaurantToken = "restaurant1token"
            });
            restaurantList.Add(new Restaurant()
            {
                Id = 2,
                RestaurantName = "Test restaurant 2",
                Description = "Test restaurant 2 description",
                OpeningTimes = "1900",
                ClosingTimes  = "2300",
                PhoneNumber = "01219999998",
                AddressLine1 = "Test restaurant 2 address",
                Area ="Test restaurant2 area",
                Postcode = "Test restaurant2 postcode",
                WebsiteURL = "www.test-restaurant2.co.uk",
                PhotoURL = "wwww.test-restaurant2.jpg",
                AdditionalInfo = "Test restaurant2 additional info",
                Cuisine = "Romania",
                Capacity = 10,
                RestaurantToken = "restaurant2token"
            });

            int id = 2;

            var mockRepository = new Mock<IRepository<Restaurant>>();

            mockRepository.Setup(repo => repo.Get(id))
                          .Returns(Task.FromResult((restaurantList.FirstOrDefault(
                               s => s.Id == id))));
            
            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.Get(id);

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Restaurant>(viewResult.Value);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.Equal(2, model.Id);
            Assert.Equal("Test restaurant 2", model.RestaurantName);
            Assert.Equal(10, model.Capacity);
        }

        [Fact]
        public async void WhenGetFunctionIsCalled_IfExceptionThrown_ReturnsNotFoundStatus()
        {
            int id = 3;

            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.Get(id)).Throws(new Exception());

            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.Get(id);

            var viewResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, viewResult.StatusCode);
        }
    }
}
