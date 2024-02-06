using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data.Migrations;
using Microsoft.AspNetCore.Identity;

namespace CarRepair.Pages.Models
{
    public class Repair
    {
        public int Id { get; set; }
        public DateOnly VisitDate { get; set; }
        public string? WorkerNote { get; set; }
        public decimal Price { get; set; }
        public DateOnly AcceptDate { get; set; }
        public Status CarStatus { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public int IssueId { get; set; }
        public Issue? Issue { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public string? AssignedMechanicId { get; set; }
        public AppUser? AssignedMechanic { get; set; }
    }
}