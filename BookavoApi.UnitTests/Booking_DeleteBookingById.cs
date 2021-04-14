using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookavoApi.UnitTests
{
    public class Booking_DeleteBookingById
    {
        [Fact]
        public void WhenDeleteFunctionIsCalled_IfValidId_ReturnsOkStatus()
        {
            int bookingId = 1;
            var mockRepository = new Mock<IRepositoryB<Booking>>();
            mockRepository.Setup(repo => repo.Delete(bookingId));

            var controller = new BookingController(mockRepository.Object);

            var result = controller.Delete(bookingId);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = viewResult.Value;

            mockRepository.Verify(repo => repo.Delete(bookingId), Times.Once);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.Equal(model, $"Booking {bookingId} has been deleted");
        }
        
        [Fact]
        public void WhenDeleteFunctionIsCalled_IfExceptionThrown_ReturnsNotFoundStatus()
        {
            int bookingId = 1;
            var mockRepository = new Mock<IRepositoryB<Booking>>();
            mockRepository.Setup(repo => repo.Delete(bookingId)).Throws(new Exception());

            var controller = new BookingController(mockRepository.Object);

            var result = controller.Delete(bookingId);

            var viewResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, viewResult.StatusCode);
        }

    }
}
