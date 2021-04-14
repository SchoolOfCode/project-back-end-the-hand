using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]s")]
    public class BookingController : ControllerBase
    {
       private readonly IRepositoryB<Booking> _bookingRepository;

        public BookingController(IRepositoryB<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int restaurantId, string date, string token)
        {
            if ((!String.IsNullOrEmpty(restaurantId.ToString())) && !String.IsNullOrEmpty(date) && String.IsNullOrEmpty(token)){
                try {
                    var bookingList = await _bookingRepository.GetByRestaurantAndDate(restaurantId, date);
                    return Ok(bookingList);
                }
                catch (Exception){
                    return NotFound($"There are no bookings for restaurant {restaurantId} for the date {date}");
                }
            } else {
                try {
                    var bookingList = await _bookingRepository.GetByRestaurantToken(token);
                    return Ok(bookingList);
                }
                catch (Exception){
                    return NotFound($"There are currently no bookings for this restaurant");
                }
            }
        }

    [HttpGet("{id}")]   //get bookings by restaurant id
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
    public IActionResult Delete(int id)
    {
        try{
            _bookingRepository.Delete(id);
            return Ok($"Booking {id} has been deleted");
        }
        catch (Exception) {
            return NotFound($"There is no booking with id {id}");
        }
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

