using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.MVC.Models;

namespace CarRepair.MVC.ViewModels
{
    public class PagerVM
    {
        public Pager? Pager { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
    }
}