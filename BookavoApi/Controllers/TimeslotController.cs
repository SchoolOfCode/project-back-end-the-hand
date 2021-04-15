using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]s")]
    public class TimeslotController : ControllerBase
    {
       private readonly IRepositoryT<Timeslot> _timeslotRepository;

        public TimeslotController(IRepositoryT<Timeslot> timeslotRepository)
        {
            _timeslotRepository = timeslotRepository;
        }
    

    [HttpGet]
    public async Task<IActionResult> GetAllByRestaurant([FromQuery] int restaurantId, string date)
        {
                try {
                    var slotList = await _timeslotRepository.GetBookedSlotsByDay(restaurantId, date);
                    return Ok(slotList);
                }
                catch (Exception){
                    return NotFound($"There are no bookings for this day");
                }
        }
}