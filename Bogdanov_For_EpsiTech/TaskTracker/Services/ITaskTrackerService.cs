namespace TaskTracker.Services
{
    public interface ITaskTrackerService<T>
    {
        Task<T?> GetTaskByIdAsync(int id);
        Task<IEnumerable<T>> GetAllTasksAsync();
        Task CreateTaskAsync(T task);
        Task UpdateTaskAsync(T task);
        Task DeleteTaskAsync(int id);
    }
}
