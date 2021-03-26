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
       private readonly IRepository<Booking> _bookingRepository;

        public BookingController(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
    

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string search)
        {
            if (!String.IsNullOrEmpty(search)){
                try {
                    var bookingList = await _bookingRepository.GetSearch(search);
                    return Ok(bookingList);
                }
                catch (Exception){
                    return NotFound($"There are no bookings which match that search");
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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)  
    {
        try {
            var booking = await _bookingRepository.Get(id);
            return Ok(booking);
        }
        catch (Exception){
            return NotFound($"There is no booking with id {id}");
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
       
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var newBooking = await _bookingRepository.Insert(booking);
        return Created($"/bookings/{booking.BookingId}", newBooking);
    }
}

