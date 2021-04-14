using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Restaurant_GetAllRestaurants
    {
        public List<Restaurant> GetRestaurantsList()
        {
            var restaurantList = new List<Restaurant>();
            restaurantList.Add(new Restaurant()
            {
                Id = 1,
                RestaurantName = "Test restaurant 1",
                Description = "Test restaurant 1 description",
                OpeningTimes = "1700",
                ClosingTimes = "1800",
                PhoneNumber = "01219999999",
                AddressLine1 = "Test restaurant 1 address",
                Area = "Test restaurant1 area",
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
                ClosingTimes = "2300",
                PhoneNumber = "01219999998",
                AddressLine1 = "Test restaurant 2 address",
                Area = "Test restaurant2 area",
                Postcode = "Test restaurant2 postcode",
                WebsiteURL = "www.test-restaurant2.co.uk",
                PhotoURL = "wwww.test-restaurant2.jpg",
                AdditionalInfo = "Test restaurant2 additional info",
                Cuisine = "Romania",
                Capacity = 10,
                RestaurantToken = "restaurant2token"
            });
            return restaurantList;
        }


        [Fact]
        public async Task WhenGetAllFunctionIsCalled__ReturnsOKStatusAndAllRestaurants()
        {

            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.GetAll())
                          .ReturnsAsync(GetRestaurantsList());

            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.GetAll("");

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Restaurant>>(viewResult.Value);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async void WhenGetAllFunctionIsCalled_IfExceptionThrown_ReturnsNotFoundStatus()
        {
            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.GetAll()).Throws(new Exception());

            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.GetAll("");

            var viewResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, viewResult.StatusCode);
        }
    }
}
