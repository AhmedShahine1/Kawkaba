namespace InitialProject.BusinessLayer.Interfaces;

public interface IService<T>
{
    public Task<List<T>> GetAllAsync();

    public Task<T> GetByIdAsync(int id);

    public Task<int> AddAsync(T t);

    public Task<int> UpdateAsync(T t);

    public Task<int> DeleteAsync(int id);
}