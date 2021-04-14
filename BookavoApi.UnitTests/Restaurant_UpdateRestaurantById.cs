using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Booking_UpdateBookingById
    {
        [Fact]
        public async void WhenUpdateFunctionIsCalled_IfValidId_ReturnsOkStatusAndUpdatedBooking()
        {
            int bookingId = 1;
            var testBooking = new Booking() { CustomerName = "Harry Potter", NumberOfPeople = 3};

            var mockRepository = new Mock<IRepositoryB<Booking>>();
            mockRepository.Setup(repo => repo.Update(testBooking)).Returns(Task.FromResult<Booking>(testBooking));;

            var controller = new BookingController(mockRepository.Object);

            var result = await controller.Update(bookingId, testBooking);
            
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Booking>(viewResult.Value);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.Equal(bookingId, model.BookingId);
            Assert.Equal(testBooking.CustomerName, model.CustomerName);
            Assert.Equal(testBooking.NumberOfPeople, model.NumberOfPeople);
        }
        
        [Fact]
        public async void WhenUpdateFunctionIsCalled_IfExceptionThrown_ReturnsBadRequestStatus()
        {
            int bookingId = 1;
            var testBooking = new Booking() {};

            var mockRepository = new Mock<IRepositoryB<Booking>>();
            mockRepository.Setup(repo => repo.Update(testBooking)).Throws(new Exception());;

            var controller = new BookingController(mockRepository.Object);

            var result = await controller.Update(bookingId, testBooking);

            var viewResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, viewResult.StatusCode);
        }

    }
}
