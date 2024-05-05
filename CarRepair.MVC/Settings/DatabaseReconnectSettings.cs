using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.MVC.Settings
{
    public class DatabaseReconnectSettings
    {
        public int RetryCount { get; set; }
        public int RetryWaitPeriodInSeconds { get; set; }
    }
}