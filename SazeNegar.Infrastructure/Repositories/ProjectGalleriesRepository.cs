using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SazeNegar.Infrastructure.Repositories
{
    public class ProjectGalleriesRepository : BaseRepository<ProjectGallery, DbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ProjectGalleriesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ProjectGallery> GetProjectGalleries()
        {
            return _context.ProjectGalleries.Where(h => h.IsDeleted == false).ToList();
        }
        public List<ProjectGallery> GetByProjectId(int projectId)
        {
            return _context.ProjectGalleries.Where(p => p.ProjectId == projectId && p.IsDeleted == false).ToList();
        }
    }
}
