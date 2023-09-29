namespace Entities.Models
{
    public class TaskEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int AllottedTime { get; set; }
        public int ElapsedTime { get; set; }
        public bool Status { get; set; }
    }
}
