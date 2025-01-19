using Task_Manager.Repositories;

namespace Task_Manager.Test.Repositories
{
    public class TaskRepositoryTests
    {
        private const string TestFilePath = "test_tasks.json";

        public TaskRepositoryTests()
        {
            CleanupTestData();
        }

        private TaskRepository CreateTestRepository()
        {
            return new TaskRepository(TestFilePath);
        }

        private void CleanupTestData()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [Fact]
        public void AddTask_ShouldAddTaskSuccessfully()
        {
            var repository = CreateTestRepository();
            var task = new Models.Task { Id = 1, Description = "Test Task", Status = "todo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

            repository.AddTask(task);

            var tasks = repository.GetAllTasks();
            Assert.Single(tasks);
            Assert.Equal("Test Task", tasks[0].Description);
        }

        [Fact]
        public void DeleteTask_ShouldRemoveTaskSuccessfully()
        {
            var repository = CreateTestRepository();
            var task = new Models.Task { Id = 1, Description = "Test Task", Status = "todo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

            repository.AddTask(task);
            repository.DeleteTask(1);

            var tasks = repository.GetAllTasks();
            Assert.Empty(tasks);
        }

        [Fact]
        public void UpdateTask_ShouldUpdateTaskSuccessfully()
        {
            var repository = CreateTestRepository();
            var task = new Models.Task { Id = 1, Description = "Test Task", Status = "todo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

            repository.AddTask(task);

            task.Description = "Updated Task";
            repository.UpdateTask(task);

            var updatedTask = repository.GetTaskById(1);
            Assert.Equal("Updated Task", updatedTask.Description);
        }

        [Fact]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            var repository = CreateTestRepository();
            var task1 = new Models.Task { Id = 1, Description = "Task 1", Status = "todo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var task2 = new Models.Task { Id = 2, Description = "Task 2", Status = "in-progress", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

            repository.AddTask(task1);
            repository.AddTask(task2);

            var tasks = repository.GetAllTasks();
            Assert.Equal(2, tasks.Count);
        }

        [Fact]
        public void GetTaskById_ShouldReturnCorrectTask()
        {
            var repository = CreateTestRepository();
            var task = new Models.Task { Id = 1, Description = "Test Task", Status = "todo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

            repository.AddTask(task);

            var retrievedTask = repository.GetTaskById(1);
            Assert.NotNull(retrievedTask);
            Assert.Equal("Test Task", retrievedTask.Description);
        }
    }
}
