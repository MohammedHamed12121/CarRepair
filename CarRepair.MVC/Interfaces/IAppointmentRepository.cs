using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRepair.MVC.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Repair>> GetAllAsync(string userId, string dataCacheKey, int page, int pageSize, AppointmentStatus appointmentStatus = AppointmentStatus.NotSeen);
        Task<int> GetAllItemsCountAsync(string userId, AppointmentStatus appointmentStatus);
        Task<Repair> GetByIdAsync(int repairId);
        Task Add(Repair repair);

        Task Update(Repair repair);

        Task Delete(Repair repair);

        Task Save();
    }
}