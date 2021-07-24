using SazeNegar.Core.Models;
using SazeNegar.Infrastructure.Helpers;
using SazeNegar.Infratructure.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Infrastructure.Repositories;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class OurServiceController : Controller
    {
        private readonly OurServiceRepository _repo;

        public OurServiceController(OurServiceRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/Partners
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OurService ourService)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(ourService);
                return RedirectToAction("Index");
            }

            return View(ourService);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OurService ourService  = _repo.Get(id.Value);
            if (ourService == null)
            {
                return HttpNotFound();
            }
            return View(ourService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OurService ourService)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(ourService);
                return RedirectToAction("Index");
            }
            return View(ourService);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OurService ourService = _repo.Get(id.Value);
            if (ourService == null)
            {
                return HttpNotFound();
            }
            return PartialView(ourService);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var image = _repo.Get(id);

            //#region Delete Image
            //if (image.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/PartnersImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/PartnersImages/" + image.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/PartnersImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/PartnersImages/" + image.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }


    }
}