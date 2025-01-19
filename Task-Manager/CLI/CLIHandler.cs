using Task_Manager.Services;

namespace Task_Manager.CLI
{
    public class CLIHandler
    {
        private readonly ITaskService _taskService;

        public CLIHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void Handle(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No command provided. Use 'help' for a list of commands.");
                return;
            }

            var command = args[0].ToLower();

            try
            {
                switch (command)
                {
                    case "add":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Usage: task-cli add \"Task Description\"");
                            return;
                        }
                        _taskService.AddTask(args[1]);
                        Console.WriteLine("Task added successfully.");
                        break;

                    case "update":
                        if (args.Length < 3 || !int.TryParse(args[1], out var updateId))
                        {
                            Console.WriteLine("Usage: task-cli update <TaskID> \"Updated Description\"");
                            return;
                        }
                        _taskService.UpdateTask(updateId, args[2]);
                        Console.WriteLine("Task updated successfully.");
                        break;

                    case "delete":
                        if (args.Length < 2 || !int.TryParse(args[1], out var deleteId))
                        {
                            Console.WriteLine("Usage: task-cli delete <TaskID>");
                            return;
                        }
                        _taskService.DeleteTask(deleteId);
                        Console.WriteLine("Task deleted successfully.");
                        break;

                    case "mark-in-progress":
                        if (args.Length < 2 || !int.TryParse(args[1], out var inProgressId))
                        {
                            Console.WriteLine("Usage: task-cli mark-in-progress <TaskID>");
                            return;
                        }
                        _taskService.MarkInProgress(inProgressId);
                        Console.WriteLine("Task marked as in-progress.");
                        break;

                    case "mark-done":
                        if (args.Length < 2 || !int.TryParse(args[1], out var doneId))
                        {
                            Console.WriteLine("Usage: task-cli mark-done <TaskID>");
                            return;
                        }
                        _taskService.MarkDone(doneId);
                        Console.WriteLine("Task marked as done.");
                        break;

                    case "list":
                        var status = args.Length > 1 ? args[1].ToLower() : null;
                        var tasks = _taskService.ListTasks(status);
                        PrintTasks(tasks);
                        break;

                    case "help":
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("  add \"Task Description\" - Add a new task");
                        Console.WriteLine("  update <TaskID> \"Updated Description\" - Update a task");
                        Console.WriteLine("  delete <TaskID> - Delete a task");
                        Console.WriteLine("  mark-in-progress <TaskID> - Mark a task as in-progress");
                        Console.WriteLine("  mark-done <TaskID> - Mark a task as done");
                        Console.WriteLine("  list [status] - List tasks (status: todo, in-progress, done)");
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {command}. Use 'help' for a list of commands.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        void PrintTasks(IEnumerable<Models.Task> tasks)
        {
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            // Print table header
            Console.WriteLine($"{"ID",-5}|{"Description",-30}|{"Status",-15}|{"Created At",-20}|{"Updated At",-20}");
            Console.WriteLine(new string('-', 90));

            foreach (var task in tasks)
            {
                // Set color based on task status
                switch (task.Status.ToLower())
                {
                    case "todo":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "in-progress":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case "done":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    default:
                        Console.ResetColor();
                        break;
                }

                // Print task details in tabular format with | separator
                Console.WriteLine($"{task.Id,-5}|{task.Description,-30}|{task.Status,-15}|{task.CreatedAt,-20}|{task.UpdatedAt,-20}");

                // Reset color after each task
                Console.ResetColor();
            }
            Console.WriteLine(new string('-', 90));
        }
    }
}
