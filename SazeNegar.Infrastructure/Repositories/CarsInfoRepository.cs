using SazeNegar.Core.Models;
using SazeNegar.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SazeNegar.Infrastructure.Repositories
{
    public class CarsInfoRepository : BaseRepository<CarsInfo, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public CarsInfoRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public CarsInfo GetCarInfo(int id)
        {
            return _context.CarsInfos.FirstOrDefault(c => c.Id == id);
        }

        public List<CarsInfo> GetCarsInfosList()
        {
            return _context.CarsInfos.ToList();
        }
        public int GetCarsInfoCount()
        {
            return _context.CarsInfos
                    .Count(a => a.IsDeleted == false);
        }
        
    }
}
