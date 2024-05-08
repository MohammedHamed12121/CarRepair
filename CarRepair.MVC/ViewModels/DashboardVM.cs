using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.MVC.ViewModels
{
    public class DashboardVM
    {
        public int Mechanics { get; set; }
        public int NewAppointments { get; set; }
        public int CurrentRepairs { get; set; }
        public int Services { get; set; }
        public int FinishedRequests { get; set; }
    }
}