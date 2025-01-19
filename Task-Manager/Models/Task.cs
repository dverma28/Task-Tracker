namespace Task_Manager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "todo";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
