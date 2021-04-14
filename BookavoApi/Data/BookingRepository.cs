using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public class BookingRepository : BaseRepository, IRepositoryB<Booking>
{
    public BookingRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<Booking>> GetAll()
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Booking>> bookings = connection.QueryAsync<Booking>("SELECT * FROM bookingview;");
        return await bookings;
    }

    public async Task<IEnumerable<Booking>> GetByRestaurantAndDate(int restaurantId, string date)
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Booking>> bookings = connection.QueryAsync<Booking>("SELECT * FROM bookingview WHERE RestaurantId =@RestaurantId AND BookingDate ILIKE @Date;", new { RestaurantId = restaurantId, Date = $"{date}%" });

        return await bookings;
    }

    public async Task<IEnumerable<Booking>> GetByRestaurantToken(string token)
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Booking>> bookings = connection.QueryAsync<Booking>("SELECT BookingId, RestaurantId, CustomerName, BookingDate, BookingTime, NumberOfPeople, CustomerMobile, CustomerEmail FROM BookingView INNER JOIN RestaurantView ON RestaurantId=Id WHERE RestaurantToken ILIKE @RestaurantToken", new { RestaurantToken = $"{token}" });
        return await bookings;
    }

    public async Task<IEnumerable<Booking>> GetByRestaurant(int restaurantId)
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Booking>> bookings = connection.QueryAsync<Booking>("SELECT * FROM bookingview WHERE RestaurantId = @RestaurantId;", new { RestaurantId = restaurantId });
        return await bookings;
    }

    public void Delete(int id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM bookingview WHERE BookingId = @BookingId;", new { BookingId = id });
    }

    public async Task<Booking> Update(Booking booking)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Booking>("UPDATE bookingview SET RestaurantId = @RestaurantId, CustomerName = @CustomerName, BookingDate = @BookingDate, BookingTime = @BookingTime, NumberOfPeople = @NumberOfPeople, CustomerMobile = @CustomerMobile, CustomerEmail = @CustomerEmail WHERE BookingId = @BookingId RETURNING *;", booking);
    }

    public async Task<Booking> Insert(Booking booking)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Booking>(
            "INSERT INTO bookingview (RestaurantId, CustomerName, BookingDate, BookingTime, NumberOfPeople, CustomerMobile, CustomerEmail) VALUES (@RestaurantId, @CustomerName, @BookingDate, @BookingTime, @NumberOfPeople, @CustomerMobile, @CustomerEmail) RETURNING *;", booking);
    }


}
