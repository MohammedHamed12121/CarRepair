using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepair.MVC.Interfaces
{
    public interface IDatabaseRetryService
    {
        Task ExecuteWithRetryAsync(Func<Task> action);
    }
}