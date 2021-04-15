using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public class RestaurantRepository : BaseRepository, IRepository<Restaurant>
{
    public RestaurantRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<Restaurant>> GetAll()
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Restaurant>> restaurants = connection.QueryAsync<Restaurant>("SELECT * FROM restaurantview;");
        return await restaurants;
    }

    public async Task<IEnumerable<Restaurant>> GetSearch(string cuisine)
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Restaurant>> restaurants = connection.QueryAsync<Restaurant>("SELECT * FROM restaurantview WHERE Cuisine ILIKE @Cuisine;", new { Cuisine = $"%{cuisine}%" });

        return await restaurants;
    }

    public async Task<Restaurant> Get(int id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Restaurant>("SELECT * FROM restaurantview WHERE Id = @Id;", new { Id = id });
    }

    public void Delete(int id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM restaurantview WHERE Id = @Id;", new { Id = id });
    }

    public async Task<Restaurant> Update(Restaurant restaurant)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Restaurant>("UPDATE restaurantview SET RestaurantName = @RestaurantName, Description = @Description, OpeningTimes = @OpeningTimes, ClosingTimes = @ClosingTimes, PhoneNumber = @PhoneNumber, AddressLine1 = @AddressLine1, Area = @Area, Postcode =@Postcode, WebsiteURL =@WebsiteURL, PhotoURL = @PhotoURL, AdditionalInfo =@AdditionalInfo, Cuisine =@Cuisine, Capacity =@Capacity, RestaurantToken =@RestaurantToken WHERE Id = @Id RETURNING *;", restaurant);
    }

    public async Task<Restaurant> Insert(Restaurant restaurant)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Restaurant>(
            "INSERT INTO restaurantview (RestaurantName, Description, OpeningTimes, ClosingTimes, PhoneNumber, AddressLine1, Area, Postcode, WebsiteURL, PhotoURL, AdditionalInfo, Cuisine, Capacity, RestaurantToken) VALUES (@RestaurantName, @Description, @OpeningTimes, @ClosingTimes, @PhoneNumber, @AddressLine1, @Area, @Postcode, @WebsiteURL, @PhotoURL, @AdditionalInfo, @Cuisine, @Capacity, @RestaurantToken) RETURNING *;", restaurant);
    }

}
