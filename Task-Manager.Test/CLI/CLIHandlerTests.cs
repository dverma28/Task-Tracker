using Moq;
using Task_Manager.CLI;
using Task_Manager.Services;

namespace Task_Manager.Test.CLI
{
    public class CLIHandlerTests
    {
        [Fact]
        public void Handle_ShouldAddTask_WhenAddCommandIsValid()
        {
            var mockService = new Mock<ITaskService>();
            var cliHandler = new CLIHandler(mockService.Object);

            cliHandler.Handle(new[] { "add", "Test Task" });

            mockService.Verify(service => service.AddTask("Test Task"), Times.Once);
        }

        [Fact]
        public void Handle_ShouldShowError_WhenAddCommandIsInvalid()
        {
            var mockService = new Mock<ITaskService>();
            var cliHandler = new CLIHandler(mockService.Object);

            var output = CaptureConsoleOutput(() => cliHandler.Handle(new[] { "add" }));

            Assert.Contains("Usage: task-cli add", output);
            mockService.Verify(service => service.AddTask(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Handle_ShouldDeleteTask_WhenDeleteCommandIsValid()
        {
            var mockService = new Mock<ITaskService>();
            var cliHandler = new CLIHandler(mockService.Object);

            cliHandler.Handle(new[] { "delete", "1" });

            mockService.Verify(service => service.DeleteTask(1), Times.Once);
        }

        [Fact]
        public void Handle_ShouldShowError_WhenDeleteCommandIsInvalid()
        {
            var mockService = new Mock<ITaskService>();
            var cliHandler = new CLIHandler(mockService.Object);

            var output = CaptureConsoleOutput(() => cliHandler.Handle(new[] { "delete" }));

            Assert.Contains("Usage: task-cli delete", output);
            mockService.Verify(service => service.DeleteTask(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Handle_ShouldListTasks_WhenListCommandIsValid()
        {
            var mockService = new Mock<ITaskService>();
            mockService.Setup(service => service.ListTasks(null)).Returns(new List<Models.Task>
        {
            new Models.Task { Id = 1, Description = "Test Task", Status = "todo" }
        });

            var cliHandler = new CLIHandler(mockService.Object);

            var output = CaptureConsoleOutput(() => cliHandler.Handle(new[] { "list" }));

            Assert.Contains("ID: 1, Description: Test Task", output);
            mockService.Verify(service => service.ListTasks(null), Times.Once);
        }

        private string CaptureConsoleOutput(Action action)
        {
            var originalOutput = Console.Out;
            using var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            action();

            Console.SetOut(originalOutput);
            return consoleOutput.ToString();
        }
    }
}
