using SazeNegar.Core.Models;
using SazeNegar.Infrastructure.Helpers;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectsRepository _repo;

        public ProjectsController(ProjectsRepository repo)
        {
            _repo = repo;
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            var projects = _repo.GetProjects();
            var projectsListVm = new List<ProjectInfoViewModel>();

            foreach (var item in projects)
                projectsListVm.Add(new ProjectInfoViewModel(item));

            return View(projectsListVm);
        }

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            ViewBag.ProjectCategoryId = new SelectList(_repo.GetProjectCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, HttpPostedFileBase projectImage)
        {
            if (ModelState.IsValid)
            {

                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewBag.Message = "کاربر وارد کننده پیدا نشد.";
                    return View(project);
                }


                #region Upload Image
                if (projectImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(projectImage.FileName);
                    projectImage.SaveAs(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600,true);     
                    image.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ProjectImages/Image/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));

                    project.Image = newFileName;
                }
                #endregion

                _repo.AddProject(project);


                return RedirectToAction("Index");
            }
            ViewBag.ProjectCategoryId = new SelectList(_repo.GetProjectCategories(), "Id", "Name", project.ProjectCategoryId);
            return View(project);
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _repo.GetProject(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectCategoryId = new SelectList(_repo.GetProjectCategories(), "Id", "Name", project.ProjectCategoryId);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project, HttpPostedFileBase projectImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (projectImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/ProjectImages/Image/" + project.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Image/" + project.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(projectImage.FileName);
                    projectImage.SaveAs(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600,true);
                    image.Resize(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ProjectImages/Image/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ProjectImages/Temp/" + newFileName));

                    project.Image = newFileName;
                }
                #endregion
                _repo.Update(project);

                return RedirectToAction("Index");
            }
            ViewBag.ProjectCategoryId = new SelectList(_repo.GetProjectCategories(), "Id", "Name", project.ProjectCategoryId);
            return View(project);
        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            var files = HttpContext.Request.Files;
            foreach (var fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName.ToString()];
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Files/ProjectImages/" + newFileName));
                TempData["UploadedFile"] = newFileName;
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _repo.GetProject(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return PartialView(project);
        }


        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = _repo.Get(id);

            //#region Delete Article Image
            //if (article.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/ArticleImages/Image/" + article.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Image/" + article.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/ArticleImages/Thumb/" + article.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Thumb/" + article.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}