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
            CarsInfo carsInfo = new CarsInfo();
            carsInfo = _repo.GetAll();

            return View(carsInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarsInfo carsInfo, HttpPostedFileBase carImage1,
            HttpPostedFileBase carImage2,
            HttpPostedFileBase carImage3,
            HttpPostedFileBase carImage4,
            HttpPostedFileBase carImage5,
            HttpPostedFileBase carImage6)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (carImage1 != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage1.FileName);
                    carImage1.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                    carsInfo.Image1 = newFileName;
                }
                if (carImage2 != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage2.FileName);
                    carImage2.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                    carsInfo.Image2 = newFileName;
                }
                if (carImage3 != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage3.FileName);
                    carImage3.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                    carsInfo.Image3 = newFileName;
                }
                if (carImage4 != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage4.FileName);
                    carImage4.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                    carsInfo.Image4 = newFileName;
                }
                if (carImage5 != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage5.FileName);
                    carImage5.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                    carsInfo.Image5 = newFileName;
                }
                if (carImage6 != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage6.FileName);
                    carImage6.SaveAs(Server.MapPath("~/Files/CarsInfoImages/Image/" + newFileName));

                    carsInfo.Image6 = newFileName;
                }
                #endregion

                _repo.Add(carsInfo);
                return RedirectToAction("Index");
            }

            return View(carsInfo);
        }

        // GET: Admin/ArticleCategories/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBrandsViewModel carBrandsViewModel = new CarBrandsViewModel();
            carBrandsViewModel.Cars = _repo.Get(id);
            carBrandsViewModel.CarBrandsList = _repo.GetBrandsList();
            ViewBag.brandId = _repo.Get(id).BrandId;
            if (carBrandsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(carBrandsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarBrandsViewModel carBrandsViewModel, HttpPostedFileBase carImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (carImage != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(carImage.FileName);
                    carImage.SaveAs(Server.MapPath("~/Files/CarsImages/Image/" + newFileName));

                    carBrandsViewModel.Cars.Image = newFileName;
                }
                #endregion

                _repo.Update(carBrandsViewModel.Cars);
                //_repoBrands.Update(carBrandsViewModel.CarBrandsList);
                return RedirectToAction("Index");
            }
            return View(carBrandsViewModel);
        }

        // GET: Admin/ArticleCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = _repo.Get(id.Value);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return PartialView(cars);
        }

        // POST: Admin/ArticleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }

        //GET
        public ActionResult CarBrands(int carId)
        {
            var user = _repo.GetCar(carId);
            ViewBag.CarID = carId;

            var db = new MyDbContext();
            var _logsRepository = new LogsRepository(db);
            var _brandsRepo = new BrandsRepository(db, _logsRepository);
            CarBrandsViewModel brandList = new CarBrandsViewModel();
            brandList.CarBrandsList = _brandsRepo.GetAll();

            return View(brandList);
        }

        //POST
        [HttpPost]
        public ActionResult CarBrands(int carId, string selectedBrand)
        {

            if (selectedBrand == null)
            {
                return RedirectToAction("CarBrands", new { carId });
            }

            _repo.EditCarBrand(carId, selectedBrand);

            return RedirectToAction("Index");
        }

    }
}
