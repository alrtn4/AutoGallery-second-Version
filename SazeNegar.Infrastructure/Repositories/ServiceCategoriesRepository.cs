using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SazeNegar.Infrastructure.Repositories
{
   public class ServiceCategoriesRepository : BaseRepository<ServiceCategory,MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;

        public ServiceCategoriesRepository(MyDbContext context,LogsRepository logger):base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
