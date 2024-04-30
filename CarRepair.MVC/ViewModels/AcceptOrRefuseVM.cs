using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.MVC.ViewModels
{
    public class AcceptOrRefuseVM
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? WorkerNote { get; set; }
        public bool Accept { get; set; }
    }
}