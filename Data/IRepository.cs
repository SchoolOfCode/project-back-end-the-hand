using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T> 
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetSearch(string cuisine);
    Task<T> Get(int id);
    Task<T> Insert (T t);
    Task<T> Update (T t);
    void Delete(int id);
}
