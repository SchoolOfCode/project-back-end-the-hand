using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public class BookingRepository : BaseRepository, IRepository<Booking>
{
    public BookingRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<Booking>> GetAll()
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Booking>> bookings = connection.QueryAsync<Booking>("SELECT * FROM bookingview;");
        return await bookings;
    }

    public async Task<IEnumerable<Booking>> GetSearch(string search)
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Booking>> bookings = connection.QueryAsync<Booking>("SELECT * FROM bookingview WHERE BookingDate ILIKE @Search;", new { Search = $"{search}" });

        return await bookings;
    }

    public async Task<Booking> Get(int id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Booking>("SELECT * FROM bookingview WHERE BookingId = @BookingId;", new { BookingId = id });
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
