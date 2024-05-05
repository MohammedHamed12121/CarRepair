using CarRepair.MVC.Data;
using CarRepair.MVC.Interfaces;
using CarRepair.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarRepair.MVC.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly IDatabaseRetryService _databaseRetryService;

        public AppointmentRepository(
                ApplicationDbContext context, 
                IMemoryCache memoryCache, 
                IDatabaseRetryService databaseRetryService)
        {
            _context = context;
            _memoryCache = memoryCache;
            _databaseRetryService = databaseRetryService;
        }

        public async Task<List<Repair>> GetAllAsync(
                        string userId, 
                        string dataCacheKey, 
                        int page, int pageSize, 
                        AppointmentStatus appointmentStatus = AppointmentStatus.NotSeen)
        {
            if (!_memoryCache.TryGetValue(dataCacheKey, out List<Repair> repairs))
            {
                var pageSkip = (page - 1) * pageSize;

                repairs = await _context.Repairs
                              .Where(r => r.UserId == userId && r.AppointmentStatus == appointmentStatus)
                              .Include(r => r.Issue)
                              .Skip(pageSkip).Take(pageSize)
                              .ToListAsync();


                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    // TODO: put this in the configuration json
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    // TODO: put this in the configuration json
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(dataCacheKey, repairs, cacheExpiryOptions);
            }
            return repairs!;
        }

        public async Task<int> GetAllItemsCountAsync(string userId, AppointmentStatus appointmentStatus)
        {
            // TODO: Cache the count
            return await _context.Repairs
                                 .Where(r => r.UserId == userId && r.AppointmentStatus == appointmentStatus)
                                 .CountAsync();
        }
        public Task Add(Repair repair)
        {
            _context.Repairs.Add(repair);
            return Save();
        }

        public Task Delete(Repair repair)
        {
            _context.Repairs.Remove(repair);
            return Save();
        }
        public async Task Save()
        {
            await _databaseRetryService.ExecuteWithRetryAsync(async () =>
            {
               await _context.SaveChangesAsync();
            });
        }

        public Task Update(Repair repair)
        {
            _context.Repairs.Update(repair);
            return Save();
        }

        public async Task<Repair> GetByIdAsync(int repairId)
        {
            return await _context.Repairs.Where(r => r.Id == repairId).FirstOrDefaultAsync();
        }
    }
}