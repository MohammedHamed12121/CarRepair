using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.Pages.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateOnly VisitDate { get; set; }
        public int IssueId { get; set; }
        public Issue? Issue { get; set; }
    }

    public class Issue
    {
        public int Id { get; set; }
        public string? Description { get; set; }
    }

    public class Acknowledge
    {
        public int Id { get; set; }
        public string? Note { get; set; } 
        public decimal Price { get; set; }
        public DateOnly VisitDate { get; set; }
        public bool Accept { get; set; } = false;
        public DateOnly AcceptDate { get; set; }
        public Status CarStatus { get; set; }
    }

    public enum Status
    {
        Recieved,
        UnderRepair,
        Repaired,
        Recieve
    }
}