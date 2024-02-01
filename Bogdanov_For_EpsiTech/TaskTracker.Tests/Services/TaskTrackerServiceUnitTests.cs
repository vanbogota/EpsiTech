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
        public async Task CreateTaskAsync_Should_AddTaskToDbContext()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var task = new MyTask { Id = 1, Name = "Task 1", Description = "Description 1" };

                // Act
                await service.CreateTaskAsync(task);

                // Assert
                Assert.Equal(1, dbContext.Tasks.Count());
            }
        }

        [Fact]
        public async Task DeleteTaskAsync_Should_RemoveTaskFromDbContext()
        {
            // Arrange         

            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var taskId = 1;
                var task = new MyTask { Id = taskId, Name = "Task 1", Description = "Description 1" };

                dbContext.Tasks.Add(task);
                await dbContext.SaveChangesAsync();

                // Act
                await service.DeleteTaskAsync(taskId);

                // Assert
                Assert.Equal(0, dbContext.Tasks.Count());
            }
        }

        [Fact]
        public async Task GetAllTasksAsync_Should_ReturnAllTasksFromDbContext()
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
                await dbContext.SaveChangesAsync();

                // Act
                var result = await service.GetAllTasksAsync();

                // Assert
                Assert.Equal(tasks, result);
            }
        }

        [Fact]
        public async Task GetTaskByIdAsync_Should_ReturnTaskFromDbContext()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var taskId = 1;
                var task = new MyTask { Id = taskId, Name = "Task 1", Description = "Description 1" };

                dbContext.Tasks.Add(task);
                await dbContext.SaveChangesAsync();

                // Act
                var result = await service.GetTaskByIdAsync(taskId);

                // Assert
                Assert.Equal(task, result);
            }
        }

        [Fact]
        public async Task UpdateTaskAsync_Should_UpdateTaskInDbContext()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                var taskId = 1;
                var existingTask = new MyTask { Id = taskId, Name = "Task 1", Description = "Description 1" };
                var updatedTask = new MyTask { Id = taskId, Name = "Updated Task", Description = "Updated Description" };

                dbContext.Tasks.Add(existingTask);
                await dbContext.SaveChangesAsync();

                // Act
                await service.UpdateTaskAsync(updatedTask);

                // Assert
                Assert.Equal("Updated Task", existingTask.Name);
                Assert.Equal("Updated Description", existingTask.Description);
                Assert.True(existingTask.UpdatedDate > DateTime.MinValue);
            }
        }
        [Fact]
        public async Task CreateTaskAsync_Should_ThrowException_WhenTaskIsNull()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                await Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateTaskAsync(null));
            }
        }

        [Fact]
        public async Task DeleteTaskAsync_Should_ThrowException_WhenTaskNotFound()
        {
            // Arrange
           using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                await Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.DeleteTaskAsync(1));
            }
        }

        [Fact]
        public async Task UpdateTaskAsync_Should_ThrowException_WhenTaskIsNull()
        {
            // Arrange            
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                await Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateTaskAsync(null));
            }
        }

        [Fact]
        public async Task UpdateTaskAsync_Should_ThrowException_WhenTaskNotFound()
        {
            // Arrange
           using (var dbContext = new ApplicationDbContext(_options))
            {
                var service = new TaskTrackerService(dbContext);

                // Act & Assert
                await Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.UpdateTaskAsync(new MyTask { Id = 1 }));
            }
        }
    }
}
