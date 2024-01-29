
using TaskTracker.Context;
using TaskTracker.Models;

namespace TaskTracker.Services.Impl
{
    /// <summary>
    /// Сервис для работы с Task Tracker.
    /// </summary>
    public class TaskTrackerService : ITaskTrackerService<MyTask>
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskTrackerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Метод для создания новой задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void CreateTask(MyTask task)
        {
            if (task == null)
            {
                throw new NullReferenceException();
            }
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления задачи по ее Id.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void DeleteTask(int id)
        {
           MyTask? task = GetTaskById(id);
           if (task == null)
            {
                throw new KeyNotFoundException();               
            }

            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Метода для получения всех задач из базы данных.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MyTask> GetAllTasks()
        {
            return _dbContext.Tasks.ToList();
        }

        /// <summary>
        /// Метод для получения задачи по ее Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MyTask? GetTaskById(int id)
        {
            return _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Метод для обновления задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void UpdateTask(MyTask task)
        {
            if (task == null)
            {
                throw new NullReferenceException();
            }

            MyTask? exsistTask = GetTaskById(task.Id);
            if (exsistTask == null)
            {
                throw new KeyNotFoundException();
            }
                        
            exsistTask.Name = task.Name;
            exsistTask.Description = task.Description;
            exsistTask.UpdatedDate = DateTime.UtcNow;

            _dbContext.Tasks.Update(exsistTask);
            _dbContext.SaveChanges();           
        }
    }
}
