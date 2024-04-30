namespace CarRepair.MVC.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateOnly DateToFinish { get; set; }

    }
}