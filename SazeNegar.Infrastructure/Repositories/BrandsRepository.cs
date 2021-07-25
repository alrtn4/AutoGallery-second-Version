using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SazeNegar.Infrastructure.Repositories
{
    public class BrandsRepository : BaseRepository<Brands, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public BrandsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Brands> GetAll()
        {
           return _context.Brands.Where(i => i.IsDeleted == false).ToList();
        }

        public List<CarModel> GetModelsList()
        {
            return _context.CarModels.Where(i => i.IsDeleted == false).ToList();
        }

    }
}
