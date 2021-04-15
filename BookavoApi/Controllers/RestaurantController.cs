using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]s")]
    public class RestaurantController : ControllerBase
    {
       private readonly IRepository<Restaurant> _restaurantRepository;

        public RestaurantController(IRepository<Restaurant> restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string cuisine)
        {
            if (!String.IsNullOrEmpty(cuisine)){
                try {
                    var restaurantList = await _restaurantRepository.GetSearch(cuisine);
                    return Ok(restaurantList);
                }
                catch (Exception){
                    return NotFound($"There are no restaurants that match the {cuisine} cuisine");
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
    public IActionResult Delete(int id)
    {
        try{
            _restaurantRepository.Delete(id);
            return Ok($"Restaurant with {id} has been deleted");
        }
        catch (Exception) {
            return NotFound($"There is no restaurant with id {id}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Restaurant restaurant) 
    {
        try 
        {
            restaurant.Id = id;
            return Ok(await _restaurantRepository.Update(restaurant));
        }
        catch (Exception) 
        {         
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] Restaurant restaurant)    
    {
        try 
        {
           var newRestaurant = await _restaurantRepository.Insert(restaurant);
            return Created($"/restaurants/{restaurant.Id}", newRestaurant);
    
        }
        catch (Exception) 
        {         
            return BadRequest();
        }
    
    }
}

