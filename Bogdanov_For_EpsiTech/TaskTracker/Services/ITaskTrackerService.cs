namespace TaskTracker.Services
{
    public interface ITaskTrackerService<T>
    {
        T? GetTaskById(int id);
        IEnumerable<T> GetAllTasks();
        void CreateTask(T task);
        void UpdateTask(T task);
        void DeleteTask(int id);
    }
}
