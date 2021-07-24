using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SazeNegar.Infrastructure.Repositories
{
    public class ServicesRepository : BaseRepository<Service, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ServicesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public Service GetService(int id)
        {
            return _context.Services.Include(a => a.User).Include(a => a.ServiceCategory).FirstOrDefault(a => a.Id == id);
        }
        public List<Service> GetServices()
        {
            return _context.Services.Where(a => a.IsDeleted == false).Include(a => a.User).Include(a => a.ServiceCategory).OrderByDescending(a => a.AddedDate).ToList();
        }

        public List<ServiceCategory> GetServiceCategories()
        {
            return _context.ServiceCategories.Where(a => a.IsDeleted == false).ToList();
        }
        public void AddService(Service service)
        {
            var user = GetCurrentUser();
            service.InsertDate = DateTime.Now;
            service.InsertUser = user.UserName;
            service.AddedDate = DateTime.Now;
            service.UserId = user.Id;
            _context.Services.Add(service);
            _context.SaveChanges();
            _logger.LogEvent(service.GetType().Name, service.Id, "Add");
        }

        public List<Service> GetServicesByCategory(int categoryId)
        {
            return _context.Services.Where(a => a.IsDeleted == false && a.ServiceCategoryId == categoryId).Include(a => a.User).Include(a => a.ServiceCategory).OrderByDescending(a => a.AddedDate).ToList();
        }

        public ServiceCategory GetCategory(int id)
        {
            return _context.ServiceCategories.Find(id);
        }
        public List<Service> GetServicesWithCategory()
        {
            return _context.Services.Where(p => p.IsDeleted == false).Include(p => p.ServiceCategory).ToList();
        }
        public List<ServiceCategory> GetCategories()
        {
            return _context.ServiceCategories.Where(p => p.IsDeleted == false).ToList();
        }
        
    }
}
