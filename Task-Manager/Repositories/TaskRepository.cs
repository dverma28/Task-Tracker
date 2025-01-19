using System.Text.Json;

namespace Task_Manager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string filePath;

        public TaskRepository(string _filePath = "tasks.json")
        {
            filePath = _filePath;
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        /// <summary>
        /// Adds a new task to JSON file.
        /// </summary>
        /// <param name="task">The task to add.</param>
        public void AddTask(Models.Task task)
        {
            var tasks = GetAllTasks();
            tasks.Add(task);
            SaveTasks(tasks);
        }

        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        public void DeleteTask(int id)
        {
            var tasks = GetAllTasks();
            var taskToRemove = tasks.FirstOrDefault(t => t.Id == id);

            if (taskToRemove == null)
            {
                throw new ArgumentException($"Task with ID {id} does not exist.");
            }

            tasks.Remove(taskToRemove);
            SaveTasks(tasks);
        }

        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <returns>A list of all tasks.</returns>
        public List<Models.Task> GetAllTasks()
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Models.Task>>(json) ?? new List<Models.Task>();
        }

        /// <summary>
        /// Gets a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <returns>The task with the specified ID.</returns>
        public Models.Task? GetTaskById(int id)
        {
            var tasks = GetAllTasks();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="updatedTask">The updated task object.</param>
        public void UpdateTask(Models.Task updatedTask)
        {
            var tasks = GetAllTasks();
            var taskIndex = tasks.FindIndex(t => t.Id == updatedTask.Id);

            if (taskIndex == -1)
            {
                throw new ArgumentException($"Task with ID {updatedTask.Id} does not exist.");
            }

            tasks[taskIndex] = updatedTask;
            SaveTasks(tasks);
        }

        /// <summary>
        /// Saves the list of tasks to the JSON file.
        /// </summary>
        /// <param name="tasks">The list of tasks to save.</param>
        private void SaveTasks(List<Models.Task> tasks)
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
