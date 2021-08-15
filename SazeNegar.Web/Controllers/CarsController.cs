using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Core.Utility;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsRepository _carsRepo;
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly BrandsRepository _brandsRepo;

        public CarsController(CarsRepository carsRepo, StaticContentDetailsRepository contentRepo, BrandsRepository brandsRepo)
        {
            _carsRepo = carsRepo;
            _contentRepo = contentRepo;
            _brandsRepo = brandsRepo;
        }

        public ActionResult Index(int pageNumber = 1, string searchString = null)
        {
            var cars = _carsRepo.GetAll();
            var vm = new List<Cars>();
            var take = 3;
            var skip = pageNumber * take - take;
            var count = 0;

            vm = _carsRepo.GetCarsList(skip, take, null);
            
            count = _carsRepo.GetCarsCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.SearchString = searchString;
            ViewBag.Brands = _brandsRepo.GetAll();
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SpecialCars = _carsRepo.GetCarsList(0, 3);

            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(int pageNumber = 1)
        {
            var cars = _carsRepo.GetAll();
            var vm = new List<Cars>();
            var take = 3;
            var skip = pageNumber * take - take;
            var count = 0;

            count = _carsRepo.GetCarsCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Brands = _brandsRepo.GetAll();
            ViewBag.SpecialCars = _carsRepo.GetCarsList(0, 3);
            
            return View(vm);
        }


        public ActionResult Titlebar()
        {
            return View();
        }
        [Route("CarListGrid")]
        public ActionResult CarListGrid(GridViewModel grid)
        {
            var cars = new List<Cars>();

            var brand = Convert.ToInt32(grid.brand);

            var optionsArr = new List<string>();
            var optionsIntArr = new List<int>();

            if (string.IsNullOrEmpty(grid.options) == false)
            {
                optionsArr = grid.options.Split('-').ToList();
            }

            if (String.IsNullOrEmpty(grid.searchString))
                grid.searchString = null;

            cars = _carsRepo.GetCarsGrid(brand, optionsArr, grid.priceFrom, grid.priceTo, grid.searchString);

            
            #region Sorting

            if (grid.sort != "date")
            {
                switch (grid.sort)
                {
                    case "newest":
                        cars = cars.OrderByDescending(p => p.Id).ToList();
                        break;
                    case "oldest":
                        cars = cars.OrderBy(p => p.Id).ToList();
                        break;
                    case "price-high-to-low":
                        cars = cars.OrderByDescending(c => c.Price).ToList();
                        break;
                    case "price-low-to-high":
                        cars = cars.OrderBy(c => c.Price).ToList();
                        break;
                }
            }
            #endregion
            

            var count = cars.Count;
            var take = 3;
            var skip = grid.pageNumber * take - take;
            int pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = grid.pageNumber;

            cars = cars.Skip(skip).Take(take).ToList();

            var carListVm = new List<CarAndDateViewModel>();
            foreach (var car in cars)
            {
                var carsVm = new CarAndDateViewModel(car);
                carListVm.Add(carsVm);
            }

            return PartialView(carListVm);
        }
    }
}