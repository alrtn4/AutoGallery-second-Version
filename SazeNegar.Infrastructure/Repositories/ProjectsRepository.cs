using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SazeNegar.Infrastructure.Repositories
{
    public class ProjectsRepository : BaseRepository<Project, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ProjectsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public Project GetProject(int id)
        {
            return _context.Projects.Include(a => a.User).Include(a => a.ProjectCategory).Include(a=>a.ProjectGalleries).FirstOrDefault(a => a.Id == id);
        }
        public List<Project> GetProjects()
        {
            return _context.Projects.Where(a => a.IsDeleted == false).Include(a => a.User).Include(a => a.ProjectCategory).OrderByDescending(a => a.AddedDate).ToList();
        }

        public List<ProjectCategory> GetProjectCategories()
        {
            return _context.ProjectCategories.Where(a => a.IsDeleted == false).ToList();
        }
        public void AddProject(Project project)
        {
            var user = GetCurrentUser();
            project.InsertDate = DateTime.Now;
            project.InsertUser = user.UserName;
            project.AddedDate = DateTime.Now;
            project.UserId = user.Id;
            _context.Projects.Add(project);
            _context.SaveChanges();
            _logger.LogEvent(project.GetType().Name, project.Id, "Add");
        }

        public List<Project> GetTopProjects(int? take = null)
        {
            return take != null ? _context.Projects.Where(a => a.IsDeleted == false).OrderByDescending(a => a.ViewCount).Take(take.Value).ToList() : _context.Projects.OrderByDescending(a => a.ViewCount).ToList();
        }
        public List<Project> GetProjectsByCategory(int categoryId)
        {
            return _context.Projects.Where(a => a.IsDeleted == false && a.ProjectCategoryId == categoryId).Include(a => a.User).Include(a => a.ProjectCategory).OrderByDescending(a => a.AddedDate).ToList();
        }

        public string GetAuthorRole(string userId)
        {
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            var role = _context.Role.FirstOrDefault(r => r.Id == userRole.RoleId);
            return role.RoleNameLocal;
        }

        public int GetProjectsCount(int? categoryId = null)
        {
            if (categoryId == null)
                return _context.Projects.Count(a => a.IsDeleted == false);
            else
                return _context.Projects
                    .Count(a => a.IsDeleted == false && a.ProjectCategoryId == categoryId.Value);
        }

        public ProjectCategory GetCategory(int id)
        {
            return _context.ProjectCategories.Find(id);
        }
        public List<Project> GetProjectsWithCategory()
        {
            return _context.Projects.Where(p => p.IsDeleted == false).Include(p => p.ProjectCategory).ToList();
        }
        public List<ProjectCategory> GetCategories()
        {
            return _context.ProjectCategories.Where(p => p.IsDeleted == false).ToList();
        }
        public void UpdateProjectViewCount(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            project.ViewCount++;
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
