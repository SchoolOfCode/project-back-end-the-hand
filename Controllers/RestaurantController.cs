using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;


namespace server.Controllers
{
    [ApiController]
    [Route("restaurants")]
    public class RestaurantController : ControllerBase
    {
       private readonly IRepository<Restaurant> _restaurantRepository;

        public RestaurantController(IRepository<Restaurant> restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
    

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string search)
        {
            if (!String.IsNullOrEmpty(search)){
                try {
                    var restaurantList = await _restaurantRepository.GetSearch(search);
                    return Ok(restaurantList);
                }
                catch (Exception){
                    return NotFound($"There are no restaurants which match that search");
                }
            } else {
                try {
                    var restaurantList = await _restaurantRepository.GetAll();
                    return Ok(restaurantList);
                }
                catch (Exception){
                    return NotFound($"There are no restaurants listed");
                }
            }
        }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)  
    {
        try {
            var restaurant = await _restaurantRepository.Get(id);
            return Ok(restaurant);
        }
        catch (Exception){
            return NotFound($"There is no restaurant with id {id}");
        }
    }

    [HttpDelete("{id}")]
    public void Delete(long id)
    {
        _restaurantRepository.Delete(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Restaurant restaurant) 
    {
        try{
            var updatedRestaurant = await _restaurantRepository.Update(new Restaurant { 
                RestaurantName
                Description
                OpeningTimes
                ClosingTimes
                PhoneNumber
                AddressLine1
                Area
                Postcode
                WebsiteURL
                PhotoURL
                Capacity
                Tables2
                Tables4
                Tables6
                Tables8 
            });
            return Ok(updatedRestaurant);
        }
        catch (Exception) {         
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] Restaurant restaurant)    
    {
       
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var newRestaurant = await _restaurantRepository.Insert(restaurant);
        return Created($"/restaurants/{restaurant.Id}", newRestaurant);
    }
    }
}
