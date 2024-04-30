using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.MVC.Models;

namespace CarRepair.MVC.ViewModels
{
    public class AdminIndexVM
    {
        public List<Repair>? Appointments { get; set; }
        public List<Repair>? Seen { get; set; }
        public List<Repair>? Accepted { get; set; }
        public List<Repair>? Rejects { get; set; }
    }
}