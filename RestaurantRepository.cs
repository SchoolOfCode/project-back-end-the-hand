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
        Task<IEnumerable<Restaurant>> restaurants = connection.QueryAsync<Restaurant>("SELECT * FROM restaurant_capacity_view;");
        return await restaurants;
    }

    public async Task<IEnumerable<Restaurant>> GetSearch(string search)
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Restaurant>> restaurants = connection.QueryAsync<Restaurant>("SELECT * FROM restaurant_capacity_view WHERE RestaurantName ILIKE @Search;", new { Search = $"%{search}%" });

        return await restaurants;
    }

    public async Task<Restaurant> Get(int id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Restaurant>("SELECT * FROM restaurant_capacity_view WHERE Id = @Id;", new { Id = id });
    }

    public void Delete(int id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM restaurant_capacity_view WHERE Id = @Id;", new { Id = id });
    }

    public async Task<Restaurant> Update(Restaurant restaurant)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Restaurant>("UPDATE restaurant_capacity_view SET RestaurantName = @RestaurantName, Description = @Description, OpeningTimes = @OpeningTimes, ClosingTimes = @ClosingTimes, PhoneNumber = @PhoneNumber, AddressLine1 = @AddressLine1, Area = @Area, Postcode =@Postcode, WebsiteURL =@WebsiteURL, PhotoURL = @PhotoURL, Capacity =@Capacity, Tables2 =@Tables2, Tables4 =@Tables4, Tables6 =@Tables6, Tables8 =@Tables8,  WHERE Id = @Id RETURNING *;", restaurant);
    }

    public async Task<Restaurant> Insert(Restaurant restaurant)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Restaurant>(
            "INSERT INTO restaurant_capacity_view (RestaurantName, Description, OpeningTimes, ClosingTimes, PhoneNumber, AddressLine1, Area, Postcode, WebsiteURL, PhotoURL, Capacity, Tables2, Tables4, Tables6, Tables8) VALUES (@RestaurantName, @Description, @OpeningTimes, @ClosingTimes, @PhoneNumber, @AddressLine1, @Area, @Postcode, @WebsiteURL, @PhotoURL, @Capacity, @Tables2, @Tables4, @Tables6, @Tables8) RETURNING *;", restaurant);
    }


}
