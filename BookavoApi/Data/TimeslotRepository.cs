using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public class TimeslotRepository : BaseRepository, IRepositoryT<Timeslot>
{
    public TimeslotRepository(IConfiguration configuration) : base(configuration) { }
    public async Task<IEnumerable<Timeslot>> GetBookedSlotsByDay (int restaurantId, string date) 
    {
        using var connection = CreateConnection();
        Task<IEnumerable<Timeslot>> slotList = connection.QueryAsync<Timeslot>("SELECT BookingTime AS timeSlot, COALESCE(SUM(NumberOfPeople),0) AS currentSlotOccupancy FROM bookingview WHERE RestaurantId = @RestaurantId AND BookingDate ILIKE @Date GROUP BY BookingTime ORDER BY BookingTime;", new { RestaurantId = restaurantId ,  Date = $"{date}%"});
        return await slotList;
    }
}
public interface IRepositoryT<T> 
{
    Task<IEnumerable<T>> GetBookedSlotsByDay (int restaurantId, string date);  
}
