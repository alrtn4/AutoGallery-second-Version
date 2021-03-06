using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Infrastructure.Repositories;
using System.Web;
using System.IO;
using SazeNegar.Infrastructure.Helpers;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly GalleriesRepository _repo;
        private readonly CarsRepository _carsRepo;
        public GalleryController(GalleriesRepository repo, CarsRepository carsRepo)
        {
            _repo = repo;
            _carsRepo = carsRepo;
        }
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public ActionResult Create()
        {
            var galleryType = new List<string>();
            galleryType.Add("Top");
            galleryType.Add("Bottom");
            ViewBag.GalleryType = galleryType;
            ViewBag.CarsList = _carsRepo.GetAll();
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery image,HttpPostedFileBase GalleryImage, string selectedType, int selectedCar)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (GalleryImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
                    GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer imageCut = new ImageResizer(1200,1200,true);

                    imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/" + newFileName));

                    ImageResizer thumb = new ImageResizer(600, 600, true);

                    thumb.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    image.Image = newFileName;
                }
                #endregion

                image.Type = selectedType;
                image.CarsId = selectedCar;
                _repo.Add(image);
                return RedirectToAction("Index");
            }

            return View(image);
        }

        public ActionResult Edit(int? id)
        {
            var galleryType = new List<string>();
            galleryType.Add("Top");
            galleryType.Add("Bottom");
            ViewBag.GalleryType = galleryType;
            ViewBag.CarsList = _carsRepo.GetAll();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery, HttpPostedFileBase GalleryImage, string selectedType, int selectedCar)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (GalleryImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + gallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + gallery.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/Thumb/" + gallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Thumb/" + gallery.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
                    GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer imageCut = new ImageResizer(1200, 1200, true);

                    imageCut.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/" + newFileName));

                    ImageResizer thumb = new ImageResizer(600, 600, true);

                    thumb.Resize(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName),
                        Server.MapPath("/Files/GalleryImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/Temp/" + newFileName));
                    gallery.Image = newFileName;
                }
                #endregion

                gallery.Type = selectedType;
                gallery.CarsId = selectedCar;

                _repo.Update(gallery);
                return RedirectToAction("Index");
            }
            return View(gallery);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var image = _repo.Get(id);

            //#region Delete Image
            //if (image.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}