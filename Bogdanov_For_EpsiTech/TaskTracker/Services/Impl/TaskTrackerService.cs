
using Microsoft.EntityFrameworkCore;
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
        public async Task CreateTaskAsync(MyTask task)
        {
            if (task == null)
            {
                throw new NullReferenceException();
            }
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Метод для удаления задачи по ее Id.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteTaskAsync(int id)
        {
           MyTask? task = await GetTaskByIdAsync(id);
           if (task == null)
            {
                throw new KeyNotFoundException();               
            }

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Метод для получения всех задач из базы данных.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MyTask>> GetAllTasksAsync()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        /// <summary>
        /// Метод для получения задачи по ее Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MyTask?> GetTaskByIdAsync(int id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Метод для обновления задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task UpdateTaskAsync(MyTask task)
        {
            if (task == null)
            {
                throw new NullReferenceException();
            }

            MyTask? exsistTask = await GetTaskByIdAsync(task.Id);
            if (exsistTask == null)
            {
                throw new KeyNotFoundException();
            }
                        
            exsistTask.Name = task.Name;
            exsistTask.Description = task.Description;
            exsistTask.UpdatedDate = DateTime.UtcNow;

            _dbContext.Tasks.Update(exsistTask);
            await _dbContext.SaveChangesAsync();           
        }
    }
}
