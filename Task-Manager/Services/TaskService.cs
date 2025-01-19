using System.Text;
using Task_Manager.Repositories;

namespace Task_Manager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Adds a new task with the given description
        /// </summary>
        /// <param name="descrption"></param>
        public void AddTask(string descrption)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(descrption)) 
            {
                throw new ArgumentException("Task description cannot be null or empty");
            }

            // Retrieve all tasks to determine the next ID
            var tasks = _repository.GetAllTasks();
            var nextId = tasks.Any() ? tasks.Max(t=> t.Id) + 1 : 1;

            var filteredDes = ParseDescription(descrption);

            // Create the new task
            var newTask = new Models.Task
            {
                Id = nextId,
                Description = filteredDes,
                Status = "todo", // Default status
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Add the task to the repository
            _repository.AddTask(newTask);
        }

        /// <summary>
        /// Delete the task based on task id
        /// </summary>
        /// <param name="id">task id</param>
        public void DeleteTask(int id)
        {
            _repository.DeleteTask(id);
        }

        /// <summary>
        /// Update the task based on task id and description
        /// </summary>
        /// <param name="id">task id</param>
        /// <param name="description">description of task</param>
        /// <exception cref="ArgumentException">Error when task not found for given id</exception>
        public void UpdateTask(int id, string description)
        {
            var task = _repository.GetTaskById(id);
            if (task == null)
            {
                throw new ArgumentException($"Task with ID {id} does not exist.");
            }

            task.Description = description;
            task.UpdatedAt = DateTime.UtcNow;
            _repository.UpdateTask(task);
        }

        /// <summary>
        /// Making status of task to in-progress
        /// </summary>
        /// <param name="id">task id</param>
        public void MarkInProgress(int id)
        {
            UpdateTaskStatus(id, "in-progress");
        }

        /// <summary>
        /// Making status of task to done status
        /// </summary>
        /// <param name="id">task id</param>
        public void MarkDone(int id)
        {
            UpdateTaskStatus(id, "done");
        }

        /// <summary>
        /// Get All task based on status
        /// </summary>
        /// <param name="status">status of task</param>
        /// <returns>All task based on status, if you don't pass status it will return all task</returns>
        public List<Models.Task> ListTasks(string? status = null)
        {
            var tasks = _repository.GetAllTasks();
            return string.IsNullOrEmpty(status) ? tasks : tasks.Where(t => t.Status == status).ToList();
        }

        /// <summary>
        /// Updates the task status based on taks id and status
        /// </summary>
        /// <param name="id">task id</param>
        /// <param name="status">status of task (todo, in-progress, done)</param>
        /// <exception cref="ArgumentException">Error when task not found for given id</exception>
        private void UpdateTaskStatus(int id, string status)
        {
            var task = _repository.GetTaskById(id);
            if (task == null)
            {
                throw new ArgumentException($"Task with ID {id} does not exist.");
            }

            task.Status = status;
            task.UpdatedAt = DateTime.UtcNow;
            _repository.UpdateTask(task);
        }

        /// <summary>
        /// This will validate the description and parse it into proper description
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Proper description</returns>
        /// <exception cref="ArgumentException">If provided description has unclosed quotes</exception>
        private string ParseDescription(string description)
        {
            bool isInsideQuote = false;
            bool isEscaping = false;
            var parsedDescription = new StringBuilder();

            foreach (var ch in description)
            {
                if (isEscaping)
                {
                    parsedDescription.Append(ch);
                    isEscaping = false;
                }
                else if (ch == '\\') // Escape character
                {
                    isEscaping = true;
                }
                else if (ch == '"')
                {
                    isInsideQuote = !isInsideQuote;
                }
                else
                {
                    parsedDescription.Append(ch);
                }
            }

            if (isInsideQuote)
            {
                throw new ArgumentException("Unclosed quote in task description.");
            }

            return parsedDescription.ToString();
        }
    }
}
