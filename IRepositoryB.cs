using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepositoryB<T> 
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetByRestaurant(int id);
    Task<IEnumerable<T>> GetByRestaurantAndDate (int restaurantId, string date);
    Task<T> Insert (T t);
    Task<T> Update (T t);
    void Delete(int id);
}
