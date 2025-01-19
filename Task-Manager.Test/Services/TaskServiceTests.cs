using Moq;
using Task_Manager.Repositories;
using Task_Manager.Services;

namespace Task_Manager.Test.Services
{
    public class TaskServiceTests
    {
        [Fact]
        public void AddTask_ShouldAddTaskSuccessfully()
        {
            var mockRepo = new Mock<ITaskRepository>();
            var service = new TaskService(mockRepo.Object);

            service.AddTask("Test Task");

            mockRepo.Verify(repo => repo.AddTask(It.Is<Models.Task>(t =>
                t.Description == "Test Task" &&
                t.Status == "todo" &&
                t.CreatedAt != default &&
                t.UpdatedAt != default
            )), Times.Once);
        }

        [Fact]
        public void AddTask_ShouldThrowException_WhenDescriptionIsEmpty()
        {
            var mockRepo = new Mock<ITaskRepository>();
            var service = new TaskService(mockRepo.Object);

            Assert.Throws<ArgumentException>(() => service.AddTask(""));
        }

        [Fact]
        public void DeleteTask_ShouldRemoveTaskSuccessfully()
        {
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(repo => repo.GetTaskById(1)).Returns(new Models.Task { Id = 1 });

            var service = new TaskService(mockRepo.Object);

            service.DeleteTask(1);

            mockRepo.Verify(repo => repo.DeleteTask(1), Times.Once);
        }

        [Fact]
        public void ListTasks_ShouldReturnAllTasks()
        {
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(repo => repo.GetAllTasks()).Returns(new List<Models.Task> { new Models.Task { Id = 1, Status = "todo" } });

            var service = new TaskService(mockRepo.Object);

            var tasks = service.ListTasks();

            Assert.Single(tasks);
            Assert.Equal("todo", tasks[0].Status);
        }
    }
}
