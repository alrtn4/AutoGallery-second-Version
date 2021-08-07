using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using SazeNegar.Core.Models;
using SazeNegar.Infrastructure;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CarsInfoController : Controller
    {
        private readonly CarsInfoRepository _repo;
        private readonly BrandsRepository _repoBrands;
        public CarsInfoController(CarsInfoRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/ArticleCategories
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/ArticleCategories/Create
        public ActionResult Create()
        {
            //CarsInfo carsInfo = new CarsInfo();
            //carsInfo = _repo.GetAll();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarsInfo carsInfo
            //HttpPostedFileBase carImage1,
            //HttpPostedFileBase carImage2,
            //HttpPostedFileBase carImage3,
            //HttpPostedFileBase carImage4,
            //HttpPostedFileBase carImage5,
            //HttpPostedFileBase carImage6,
            //HttpPostedFileBase carImage7,
            //HttpPostedFileBase carImage8,
            //HttpPostedFileBase carImage9,
            //HttpPostedFileBase carImage10,
            //HttpPostedFileBase carImage11,
            //HttpPostedFileBase carImage12
            )
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                //if (carImage1 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage1.FileName);
                //    carImage1.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop1 = newFileName;
                //}
                //if (carImage2 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage2.FileName);
                //    carImage2.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop2 = newFileName;
                //}
                //if (carImage3 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage3.FileName);
                //    carImage3.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop3 = newFileName;
                //}
                //if (carImage4 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage4.FileName);
                //    carImage4.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop4 = newFileName;
                //}
                //if (carImage5 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage5.FileName);
                //    carImage5.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop5 = newFileName;
                //}
                //if (carImage6 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage6.FileName);
                //    carImage6.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop6 = newFileName;
                //}
                //if (carImage7 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage7.FileName);
                //    carImage7.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav1 = newFileName;
                //}
                //if (carImage8 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage8.FileName);
                //    carImage8.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav2 = newFileName;
                //}
                //if (carImage9 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage9.FileName);
                //    carImage9.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav3 = newFileName;
                //}
                //if (carImage10 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage10.FileName);
                //    carImage10.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav4 = newFileName;
                //}
                //if (carImage11 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage11.FileName);
                //    carImage11.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav5 = newFileName;
                //}
                //if (carImage12 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage12.FileName);
                //    carImage12.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav6 = newFileName;
                //}
                #endregion

                _repo.Add(carsInfo);
                return RedirectToAction("Index");
            }

            return View(carsInfo);
        }

        // GET: Admin/ArticleCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarsInfo carsInfo = _repo.Get(id.Value);

            return View(carsInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarsInfo carsInfo, HttpPostedFileBase carImage1
            //HttpPostedFileBase carImage2,
            //HttpPostedFileBase carImage3,
            //HttpPostedFileBase carImage4,
            //HttpPostedFileBase carImage5,
            //HttpPostedFileBase carImage6,
            //HttpPostedFileBase carImage7,
            //HttpPostedFileBase carImage8,
            //HttpPostedFileBase carImage9,
            //HttpPostedFileBase carImage10,
            //HttpPostedFileBase carImage11,
            //HttpPostedFileBase carImage12
            )
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                //if (carImage1 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage1.FileName);
                //    carImage1.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop1 = newFileName;
                //}
                //if (carImage2 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage2.FileName);
                //    carImage2.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop2 = newFileName;
                //}
                //if (carImage3 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage3.FileName);
                //    carImage3.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop3 = newFileName;
                //}
                //if (carImage4 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage4.FileName);
                //    carImage4.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop4 = newFileName;
                //}
                //if (carImage5 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage5.FileName);
                //    carImage5.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop5 = newFileName;
                //}
                //if (carImage6 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage6.FileName);
                //    carImage6.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageTop6 = newFileName;
                //}
                //if (carImage7 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage7.FileName);
                //    carImage7.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav1 = newFileName;
                //}
                //if (carImage8 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage8.FileName);
                //    carImage8.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav2 = newFileName;
                //}
                //if (carImage9 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage9.FileName);
                //    carImage9.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav3 = newFileName;
                //}
                //if (carImage10 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage10.FileName);
                //    carImage10.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav4 = newFileName;
                //}
                //if (carImage11 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage11.FileName);
                //    carImage11.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav5 = newFileName;
                //}
                //if (carImage12 != null)
                //{
                //    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage12.FileName);
                //    carImage12.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                //    carsInfo.ImageNav6 = newFileName;
                //}
                #endregion

                _repo.Update(carsInfo);
                return RedirectToAction("Index");
            }
            return View(carsInfo);
        }

        // GET: Admin/ArticleCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarsInfo carsInfo = _repo.Get(id.Value);
            if (carsInfo == null)
            {
                return HttpNotFound();
            }
            return PartialView(carsInfo);
        }

        // POST: Admin/ArticleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
