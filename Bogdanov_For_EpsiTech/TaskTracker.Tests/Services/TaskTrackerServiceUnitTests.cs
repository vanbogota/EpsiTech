using Microsoft.EntityFrameworkCore;
using TaskTracker.Context;
using TaskTracker.Models;
using TaskTracker.Services.Impl;

namespace TaskTracker.Tests.Services
{
    public class TaskTrackerServiceUnitTests
    {
        private DbContextOptions<ApplicationDbContext> _options;

        public TaskTrackerServiceUnitTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }


        [Fact]
        public void CreateTask_Should_AddTaskToDbContext()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var task = new MyTask { Id = 1, Name = "Task 1", Description = "Description 1" };

                // Act
                service.CreateTask(task);

                // Assert
                Assert.Equal(1, dbContext.Tasks.Count());
            }
        }

        [Fact]
        public void DeleteTask_Should_RemoveTaskFromDbContext()
        {
            // Arrange         

            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var taskId = 1;
                var task = new MyTask { Id = taskId, Name = "Task 1", Description = "Description 1" };

                dbContext.Tasks.Add(task);
                dbContext.SaveChanges();

                // Act
                service.DeleteTask(taskId);

                // Assert
                Assert.Equal(0, dbContext.Tasks.Count());
            }
        }

        [Fact]
        public void GetAllTasks_Should_ReturnAllTasksFromDbContext()
        {
            // Arrange            
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var tasks = new List<MyTask>
            {
                new MyTask { Id = 1, Name = "Task 1", Description = "Description 1" },
                new MyTask { Id = 2, Name = "Task 2", Description = "Description 2" }
            };

                dbContext.Tasks.AddRange(tasks);
                dbContext.SaveChanges();

                // Act
                var result = service.GetAllTasks();

                // Assert
                Assert.Equal(tasks, result);
            }
        }

        [Fact]
        public void GetTaskById_Should_ReturnTaskFromDbContext()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var taskId = 1;
                var task = new MyTask { Id = taskId, Name = "Task 1", Description = "Description 1" };

                dbContext.Tasks.Add(task);
                dbContext.SaveChanges();

                // Act
                var result = service.GetTaskById(taskId);

                // Assert
                Assert.Equal(task, result);
            }
        }

        [Fact]
        public void UpdateTask_Should_UpdateTaskInDbContext()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var taskId = 1;
                var existingTask = new MyTask { Id = taskId, Name = "Task 1", Description = "Description 1" };
                var updatedTask = new MyTask { Id = taskId, Name = "Updated Task", Description = "Updated Description" };

                dbContext.Tasks.Add(existingTask);
                dbContext.SaveChanges();

                // Act
                service.UpdateTask(updatedTask);

                // Assert
                Assert.Equal("Updated Task", existingTask.Name);
                Assert.Equal("Updated Description", existingTask.Description);
                Assert.True(existingTask.UpdatedDate > DateTime.MinValue);
            }
        }
        [Fact]
        public void CreateTask_Should_ThrowException_WhenTaskIsNull()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                Assert.Throws<NullReferenceException>(() => service.CreateTask(null));
            }
        }

        [Fact]
        public void DeleteTask_Should_ThrowException_WhenTaskNotFound()
        {
            // Arrange
           using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                Assert.Throws<KeyNotFoundException>(() => service.DeleteTask(1));
            }
        }

        [Fact]
        public void UpdateTask_Should_ThrowException_WhenTaskIsNull()
        {
            // Arrange            
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                Assert.Throws<NullReferenceException>(() => service.UpdateTask(null));
            }
        }

        [Fact]
        public void UpdateTask_Should_ThrowException_WhenTaskNotFound()
        {
            // Arrange
           using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                Assert.Throws<KeyNotFoundException>(() => service.UpdateTask(new MyTask { Id = 1 }));
            }
        }
    }
}
