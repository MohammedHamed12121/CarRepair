using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.Pages.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateOnly DateToFinish { get; set; }

    }
}