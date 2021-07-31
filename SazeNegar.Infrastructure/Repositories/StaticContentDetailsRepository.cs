using SazeNegar.Core.Models;
using SazeNegar.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SazeNegar.Infrastructure.Repositories
{
    public class StaticContentDetailsRepository : BaseRepository<StaticContentDetail, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public StaticContentDetailsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public StaticContentDetail GetStaticContentDetail(int id)
        {
            return _context.StaticContentDetails.Include(a => a.StaticContentType).FirstOrDefault(a => a.Id == id);
        }
        public List<StaticContentDetail> GetStaticContentDetails()
        {
            return _context.StaticContentDetails.Where(e => e.IsDeleted == false).Include(a => a.StaticContentType).ToList();
        }
        public List<StaticContentDetail> GetContentByTypeId(int id)
        {
            return _context.StaticContentDetails.Where(e => e.IsDeleted == false && e.StaticContentTypeId == id).Include(a => a.StaticContentType).ToList();
        }
        public List<StaticContentType> GetStaticContentTypes()
        {
            return _context.StaticContentTypes.Where(e => e.IsDeleted == false).ToList();
        }
        public StaticContentType GetContentType(int contentTypeId)
        {
            return _context.StaticContentTypes.FirstOrDefault(e => e.IsDeleted == false && e.Id == contentTypeId);
        }
        public List<StaticContentDetail> GetSomeStaticContentDetail(int id, int count)
        {
            var entity = _context.Set<StaticContentDetail>().Where(i => i.StaticContentTypeId == id).OrderByDescending(i => i.Id).Take(count).ToList();

            return entity;
        }

        public List<StaticContentDetail> GetAboutUs(int id, int number)
        {
            var entity = _context.StaticContentDetails.Where(i => i.StaticContentTypeId == id).OrderByDescending(i => i.Id)
                .Take(number).ToList();

            return entity;
        }
    }
}
