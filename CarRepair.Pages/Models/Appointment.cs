using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.Pages.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime VisitTime { get; set; }
        public int IssueId { get; set; }
        public Issue? Issue { get; set; }
    }

    public class Issue
    {
        public int Id { get; set; }
        public string? Description { get; set; }
    }

}