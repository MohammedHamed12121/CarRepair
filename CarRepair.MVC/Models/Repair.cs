using Microsoft.AspNetCore.Identity;

namespace CarRepair.MVC.Models
{
    public class Repair
    {
        public int Id { get; set; }
        public DateOnly VisitDate { get; set; }
        public string? WorkerNote { get; set; }
        public decimal Price { get; set; }
        public DateOnly AcceptDate { get; set; }
        public CarModels CarModel { get; set; }
        public Status CarStatus { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public int IssueId { get; set; }
        public Issue? Issue { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
        public string? AssignedMechanicId { get; set; }
        public IdentityUser? AssignedMechanic { get; set; }
    }
}