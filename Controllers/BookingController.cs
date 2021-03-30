using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;

    [ApiController]
    [Route("bookings")]
    public class BookingController : ControllerBase
    {
       private readonly IRepositoryB<Booking> _bookingRepository;

        public BookingController(IRepositoryB<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
    
    //get bookings by restaurant id and date - working
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int restaurantId, string date)
        {
            if ((!String.IsNullOrEmpty(restaurantId.ToString())) && !String.IsNullOrEmpty(date)){
                try {
                    var bookingList = await _bookingRepository.GetByRestaurantAndDate(restaurantId, date);
                    return Ok(bookingList);
                }
                catch (Exception){
                    return NotFound($"There are no bookings for restaurant {restaurantId} for the date {date}");
                }
            } else {
                try {
                    var bookingList = await _bookingRepository.GetAll();
                    return Ok(bookingList);
                }
                catch (Exception){
                    return NotFound($"There are no bookings listed");
                }
            }
        }

    //get bookings by restaurant id - working
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)  
    {
        try {
            var booking = await _bookingRepository.GetByRestaurant(id);
            return Ok(booking);
        }
        catch (Exception){
            return NotFound($"There are no bookings for restaurant {id}");
        }
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _bookingRepository.Delete(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Booking booking) 
    {
        try 
        {
            booking.BookingId = id;
            return Ok(await _bookingRepository.Update(booking));
        }
        catch (Exception) 
        {         
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] Booking booking)    
    {
        try 
        {
             var newBooking = await _bookingRepository.Insert(booking);
            return Created($"/bookings/{booking.BookingId}", newBooking);
        }
        catch (Exception) 
        {         
            return BadRequest();
        }
    }
}

