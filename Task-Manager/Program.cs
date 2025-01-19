using Task_Manager.CLI;
using Task_Manager.Repositories;
using Task_Manager.Services;

class Program
{
    static void Main(string[] args)
    {
        ITaskRepository repository = new TaskRepository("tasks.json");
        ITaskService service = new TaskService(repository);
        var cliHandler = new CLIHandler(service);

        cliHandler.Handle(args);
    }
}