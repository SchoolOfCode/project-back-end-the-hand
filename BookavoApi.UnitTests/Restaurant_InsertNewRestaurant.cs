using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Restaurant_InsertNewRestaurant
    {
        [Fact]
        public async Task WhenInsertFunctionIsCalled_IfValidId_ReturnsCreatedStatusAndInsertedRestaurant(){
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

            Restaurant testRestaurant = new Restaurant{
                Id = 3,
                RestaurantName = "Test restaurant 3",
                Description = "Test restaurant 3 description",
                OpeningTimes = "1600",
                ClosingTimes  = "2200",
                PhoneNumber = "01219999997",
                AddressLine1 = "Test restaurant 3 address",
                Area ="Test restaurant3 area",
                Postcode = "Test restaurant3 postcode",
                WebsiteURL = "www.test-restaurant3.co.uk",
                PhotoURL = "wwww.test-restaurant3.jpg",
                AdditionalInfo = "Test restaurant3 additional info",
                Cuisine = "Yemen",
                Capacity = 15,
                RestaurantToken = "restaurant3token"
            };

            var mockRepository = new Mock<IRepository<Restaurant>>();

            mockRepository.Setup(repo => repo.Insert(It.IsAny<Restaurant>()))
                          .Callback(()=>restaurantList.Add(testRestaurant))
                          .Returns(()=>Task.FromResult(restaurantList.LastOrDefault()));
            
            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.Insert(testRestaurant);

            mockRepository.Verify(x => x.Insert(It.IsAny<Restaurant>()), Times.Once); 

            var viewResult = Assert.IsType<CreatedResult>(result);
            var model = Assert.IsType<Restaurant>(viewResult.Value);
            Assert.Equal(201, viewResult.StatusCode);
            Assert.Equal(testRestaurant.RestaurantName, model.RestaurantName);
            Assert.Equal(testRestaurant.Capacity, model.Capacity);
            Assert.Equal(3, restaurantList.Count);
        }

        [Fact]
        public async void WhenInsertFunctionIsCalled_IfExceptionThrown_ReturnsBadRequestStatus()
        {
            var testRestaurant = new Restaurant() {};

            var mockRepository = new Mock<IRepository<Restaurant>>();
            mockRepository.Setup(repo => repo.Insert(testRestaurant)).Throws(new Exception());

            var controller = new RestaurantController(mockRepository.Object);

            var result = await controller.Insert(testRestaurant);

            var viewResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, viewResult.StatusCode);
        }
    }
}
