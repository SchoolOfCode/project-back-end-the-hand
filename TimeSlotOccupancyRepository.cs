using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public class TimeSlotOccupancyRepository : BaseRepository, IRepositoryT<TimeSlotOccupancy>
{
    public TimeSlotOccupancyRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<TimeSlotOccupancy>> GetBookedSlotsByDay (int restaurantId, string date) //!!!!!!!!!!!!!!! TO FIGURE OUT HOW TO DO
    {
        using var connection = CreateConnection();
        Task<IEnumerable<TimeSlotOccupancy>> slotList = connection.QueryAsync<TimeSlotOccupancy>("SELECT BookingTime AS timeSlot, COALESCE(SUM(NumberOfPeople),0) AS currentSlotOccupancy FROM bookingview WHERE RestaurantId = @RestaurantId AND BookingDate ILIKE @Date GROUP BY BookingTime ORDER BY BookingTime;", new { RestaurantId = restaurantId ,  Date = $"{date}%"});
        //SELECT BookingTime, COALESCE(SUM(NumberOfPeople),0) AS TotalPeopleBooked FROM bookingView WHERE RestaurantId = 1 AND BookingDate ILIKE '2021-03-28%' GROUP BY BookingTime ORDER BY BookingTime;
        return await slotList;
    }


}
