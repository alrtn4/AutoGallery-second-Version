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
            CarBrandsViewModel carBrandsViewModel = new CarBrandsViewModel();
            carBrandsViewModel.CarsList = _repo.GetAll();
            carBrandsViewModel.CarBrandsList = _repo.GetBrandsList();

            return View(carBrandsViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarBrandsViewModel carBrandsViewModel, HttpPostedFileBase carImage, int selectedBrand)
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
