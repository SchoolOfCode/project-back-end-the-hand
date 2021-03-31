using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;

    [ApiController]
    [Route("timeslots")]
    public class TimeSlotOccupancyController : ControllerBase
    {
       private readonly IRepositoryT<TimeSlotOccupancy> _timeSlotOccupancyRepository;

        public TimeSlotOccupancyController(IRepositoryT<TimeSlotOccupancy> timeSlotOccupancyRepository)
        {
            _timeSlotOccupancyRepository = timeSlotOccupancyRepository;
        }
    

    //working
    [HttpGet]
    public async Task<IActionResult> GetAllByRestaurant([FromQuery] int restaurantId, string date)
        {
            // if (!String.IsNullOrEmpty(search) && !String.IsNullOrEmpty((id.ToString()))){
                try {
                    var slotList = await _timeSlotOccupancyRepository.GetBookedSlotsByDay(restaurantId, date);
                    return Ok(slotList);
                }
                catch (Exception){
                    return NotFound($"There are no bookings for this day");
                }
        }
}