using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepositoryT<T> 
{
    Task<IEnumerable<T>> GetBookedSlotsByDay (int restaurantId, string date);  
}
