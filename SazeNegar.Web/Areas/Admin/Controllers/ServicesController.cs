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
    public class ServicesController : Controller
    {
        private readonly ServicesRepository _repo;

        public ServicesController(ServicesRepository repo)
        {
            _repo = repo;
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            var services = _repo.GetServices();
            var serviceListVm = new List<ServiceInfoViewModel>();

            foreach (var item in services)
                serviceListVm.Add(new ServiceInfoViewModel(item));

            return View(serviceListVm);
        }

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            ViewBag.ServiceCategoryId = new SelectList(_repo.GetServiceCategories(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase serviceImage)
        {
            if (ModelState.IsValid)
            {

                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewBag.Message = "کاربر وارد کننده پیدا نشد.";
                    return View(service);
                }


                #region Upload Image
                if (serviceImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(serviceImage.FileName);
                    serviceImage.SaveAs(Server.MapPath("/Files/ServiceImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600, true);
                    image.Resize(Server.MapPath("/Files/ServiceImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ServiceImages/Image/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ServiceImages/Temp/" + newFileName));

                    service.Image = newFileName;
                }
                #endregion

                _repo.AddService(service);


                return RedirectToAction("Index");
            }
            ViewBag.ServiceCategoryId = new SelectList(_repo.GetServiceCategories(), "Id", "Title", service.ServiceCategoryId);
            return View(service);
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _repo.GetService(id.Value);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceCategoryId = new SelectList(_repo.GetServiceCategories(), "Id", "Title", service.ServiceCategoryId);
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase serviceImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (serviceImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/ServiceImages/Image/" + service.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ServiceImages/Image/" + service.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(serviceImage.FileName);
                    serviceImage.SaveAs(Server.MapPath("/Files/ServiceImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600, true);
                    image.Resize(Server.MapPath("/Files/ServiceImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ServiceImages/Image/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ServiceImages/Temp/" + newFileName));

                    service.Image = newFileName;
                }
                #endregion
                _repo.Update(service);

                return RedirectToAction("Index");
            }
            ViewBag.ServiceCategoryId = new SelectList(_repo.GetServiceCategories(), "Id", "Title", service.ServiceCategoryId);
            return View(service);
        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            var files = HttpContext.Request.Files;
            foreach (var fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName.ToString()];
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Files/ServiceImages/" + newFileName));
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
            Service service = _repo.GetService(id.Value);
            if (service == null)
            {
                return HttpNotFound();
            }
            return PartialView(service);
        }


        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var service = _repo.Get(id);

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