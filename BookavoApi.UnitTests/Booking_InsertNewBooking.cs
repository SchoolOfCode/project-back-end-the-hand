using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Booking_InsertNewBooking
    {
        [Fact]
        public async Task WhenInsertFunctionIsCalled_IfValidId_ReturnsCreatedStatusAndInsertedBooking(){
            var bookingList = new List<Booking>();
            bookingList.Add(new Booking()
            {
                BookingId = 1,
                RestaurantId = 2,
                CustomerName = "Ian McKellan",
                BookingDate ="13-05-2021",
                BookingTime = "18:00",
                NumberOfPeople = 4,
                CustomerMobile = "07740723739",
                CustomerEmail = "ian@youshallnotpass.com"
            });
            bookingList.Add(new Booking()
            {
                BookingId = 2,
                RestaurantId = 10,
                CustomerName = "Tom Cruise",
                BookingDate ="14-06-2021",
                BookingTime = "21:00",
                NumberOfPeople = 2,
                CustomerMobile = "07799999999",
                CustomerEmail = "tom@showmethemoney.com"
            });

            Booking testBooking = new Booking{
                BookingId = 3,
                RestaurantId = 4,
                CustomerName = "James Bond",
                BookingDate ="13-05-2021",
                BookingTime = "18:00",
                NumberOfPeople = 4,
                CustomerMobile = "07799999998",
                CustomerEmail = "bond@shakennotstirred.com"
            };

            var mockRepository = new Mock<IRepositoryB<Booking>>();

            mockRepository.Setup(repo => repo.Insert(It.IsAny<Booking>()))
                          .Callback(()=>bookingList.Add(testBooking))
                          .Returns(()=>Task.FromResult(bookingList.LastOrDefault()));
            
            var controller = new BookingController(mockRepository.Object);

            var result = await controller.Insert(testBooking);

            mockRepository.Verify(x => x.Insert(It.IsAny<Booking>()), Times.Once); 

            var viewResult = Assert.IsType<CreatedResult>(result);
            var model = Assert.IsType<Booking>(viewResult.Value);
            Assert.Equal(201, viewResult.StatusCode);
            Assert.Equal(testBooking.CustomerName, model.CustomerName);
            Assert.Equal(testBooking.BookingDate, model.BookingDate);
            Assert.Equal(3, bookingList.Count);

        }

        [Fact]
        public async void WhenInsertFunctionIsCalled_IfExceptionThrown_ReturnsBadRequestStatus()
        {
            var testBooking = new Booking() {};

            var mockRepository = new Mock<IRepositoryB<Booking>>();
            mockRepository.Setup(repo => repo.Insert(testBooking)).Throws(new Exception());

            var controller = new BookingController(mockRepository.Object);

            var result = await controller.Insert(testBooking);

            var viewResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, viewResult.StatusCode);
        }
    }
}
