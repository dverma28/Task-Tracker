namespace Task_Manager.Services
{
    public interface ITaskService
    {
        void AddTask(string description);
        void DeleteTask(int id);
        void UpdateTask(int id, string description);
        void MarkInProgress(int id);
        void MarkDone(int id);
        List<Models.Task> ListTasks(string? status = null);
    }
}
