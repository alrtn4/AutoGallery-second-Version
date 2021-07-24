using SazeNegar.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Utility;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly ProjectsRepository _projectsRepo;
        private readonly ProjectGalleriesRepository _projectGalleriesRepo;

        public ProjectController(ProjectsRepository projectsRepo,StaticContentDetailsRepository contentRepo,ProjectGalleriesRepository projectGalleriesRepo)
        {
            _projectsRepo = projectsRepo;
            _contentRepo = contentRepo;
            _projectGalleriesRepo = projectGalleriesRepo;
        }

        // GET: Project
        public ActionResult Index()
        {
            var projects = _projectsRepo.GetAll();
            ViewBag.Categories = _projectsRepo.GetCategories();

            return View(projects);
        }

        public ActionResult Details(int id)
        {
            var detail = _projectsRepo.GetProject(id);
            return View(detail);
        }

    }
}