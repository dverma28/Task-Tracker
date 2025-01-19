namespace Task_Manager.Repositories
{
    public interface ITaskRepository
    {
        List<Models.Task> GetAllTasks();
        Models.Task? GetTaskById(int id);
        void AddTask(Models.Task task);
        void UpdateTask(Models.Task task);
        void DeleteTask(int id);
    }
}
