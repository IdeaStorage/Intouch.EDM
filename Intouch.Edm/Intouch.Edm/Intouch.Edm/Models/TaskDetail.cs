namespace Intouch.Edm.Models
{
    public class TaskDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }
        public virtual TaskItem TaskItem { get; set; }
        public int? TaskItemId { get; set; }
    }
}