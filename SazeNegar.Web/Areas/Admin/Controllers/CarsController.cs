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
    public class CarsController : Controller
    {
        private readonly CarsRepository _repo;
        private readonly BrandsRepository _repoBrands;
        public CarsController(CarsRepository repo, BrandsRepository repoBrands)
        {
            _repo = repo;
            _repoBrands = repoBrands;
        }
        // GET: Admin/ArticleCategories
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/ArticleCategories/Create
        public ActionResult Create()
        {
            CarBrandsInfoViewModel carBrandsInfoViewModel = new CarBrandsInfoViewModel();
            carBrandsInfoViewModel.CarsList = _repo.GetAll();
            carBrandsInfoViewModel.BrandsList = _repo.GetBrandsList();
            carBrandsInfoViewModel.CarsInfoList = _repo.GetCarInfoList();

            return View(carBrandsInfoViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarBrandsInfoViewModel carBrandsViewModel, HttpPostedFileBase carImage, int selectedBrand, int selectedCarInfo)
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

                carBrandsViewModel.Cars.BrandId = selectedBrand;
                carBrandsViewModel.Cars.CarInfoId = selectedCarInfo;
                carBrandsViewModel.Cars.PersianDateTime = carBrandsViewModel.Cars.InsertDate != null ? new PersianDateTime(carBrandsViewModel.Cars.InsertDate.Value).ToString() : "-";
                _repo.Add(carBrandsViewModel.Cars);
                return RedirectToAction("Index");
            }

            return View(carBrandsViewModel);
        }

        // GET: Admin/ArticleCategories/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBrandsInfoViewModel carBrandsViewModel = new CarBrandsInfoViewModel();
            carBrandsViewModel.Cars = _repo.Get(id);
            carBrandsViewModel.BrandsList = _repo.GetBrandsList();
            carBrandsViewModel.CarsInfoList = _repo.GetCarInfoList();
            ViewBag.brandId = _repo.Get(id).BrandId;
            if (carBrandsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(carBrandsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarBrandsInfoViewModel carBrandsViewModel, HttpPostedFileBase carImage, int selectedBrand, int selectedCarInfo)
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

                carBrandsViewModel.Cars.BrandId = selectedBrand;
                carBrandsViewModel.Cars.CarInfoId = selectedCarInfo;
                _repo.Update(carBrandsViewModel.Cars);
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
            CarBrandsInfoViewModel brandList = new CarBrandsInfoViewModel();
            brandList.BrandsList = _brandsRepo.GetAll();

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
