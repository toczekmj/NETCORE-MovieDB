using Microsoft.EntityFrameworkCore;

namespace MovieApi.Interfaces;

public interface IRepository<T>
{
    public Task<T> Create(T model);
    public Task<ICollection<T>?> RetrieveCollectionOrDefault();
    
    public Task<T?> RetrieveOrDefault(T model);
    public Task<T?> RetrieveOrDefault(int id);
    
    public Task Update();
    
    public Task<EntityState> Delete(int id);
    public Task<EntityState> Delete(T model);
}