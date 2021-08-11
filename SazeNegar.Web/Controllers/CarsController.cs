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

        // GET: Blog
        //[Route("Blog")]
        //[Route("Blog/{id}/{title}")]
        public ActionResult Index(int pageNumber = 1)
        {
            var cars = _carsRepo.GetAll();
            var vm = new List<Cars>();
            var take = 3;
            var skip = pageNumber * take - take;
            var count = 0;

            vm = _carsRepo.GetCarsList(skip, take, null);
            //if (!string.IsNullOrEmpty(null))
            //{
            //    cars = _carsRepo.GetCarsList(skip, take, null);
            //    count = _carsRepo.GetCarsCount();
            //    ViewBag.SearchString = null;
            //    ViewBag.Title = $"جستجو: {null}";
            //    var pageCount2 = (int)Math.Ceiling((double)count / take);
            //    ViewBag.PageCount = pageCount2;
            //    ViewBag.CurrentPage = pageNumber;
            //    return View(cars);
            //}
            count = _carsRepo.GetCarsCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.SearchString = "";
            ViewBag.Brands = _brandsRepo.GetAll();
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Facebook = _contentRepo.GetStaticContentDetail((int)StaticContents.Facebook).Link;
            ViewBag.Instagram = _contentRepo.GetStaticContentDetail((int)StaticContents.Instagram).Link;
            ViewBag.Twitter = _contentRepo.GetStaticContentDetail((int)StaticContents.Twitter).Link;
            //List<IQueryable<CarModel>> carClassList = null;

            return View(vm);
        }
        [HttpPost]
        //[Route("Cars/")]
        //[Route("Cars/{searchString}")]
        public ActionResult Index(int pageNumber = 1, string searchString = null)
        {
            var cars = _carsRepo.GetAll();
            var vm = new List<Cars>();
            var take = 3;
            var skip = pageNumber * take - take;
            var count = 0;

            vm = _carsRepo.GetCarsList(skip, take, searchString);
            if (!string.IsNullOrEmpty(searchString))
            {
                cars = _carsRepo.GetCarsList(skip, take, searchString);
                count = _carsRepo.GetCarsCount();
                ViewBag.SearchString = searchString;
                ViewBag.Title = $"جستجو: {searchString}";
                var pageCount2 = (int)Math.Ceiling((double)count / take);
                ViewBag.PageCount = pageCount2;
                ViewBag.CurrentPage = pageNumber;
                return View(cars);
            }
            count = _carsRepo.GetCarsCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Facebook = _contentRepo.GetStaticContentDetail((int)StaticContents.Facebook).Link;
            ViewBag.Instagram = _contentRepo.GetStaticContentDetail((int)StaticContents.Instagram).Link;
            ViewBag.Twitter = _contentRepo.GetStaticContentDetail((int)StaticContents.Twitter).Link;
            List<IQueryable<CarModel>> carClassList = null;

            return View(vm);
        }

        //public ActionResult Sidebar()
        //{
        //    var sidebarContent = _carsRepo.GetCarsList(0, 3, null);
        //    return View(sidebarContent);
        //}

        //[Route("Cars/")]
        //[Route("Cars/{searchString}")]
        //public ActionResult Index(string searchString = null)
        //{
        //    var vm = new ProductListViewModel();
        //    vm.Brands = _brandsRepo.GetAll();

        //    ViewBag.Title = "محصولات";

        //    if (searchString != null)
        //        ViewBag.SearchString = searchString;

        //    ViewBag.SearchString = searchString;

        //    return View(vm);
        //}
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
                //optionsArr.ForEach(op => optionsIntArr.Add(Convert.ToInt32(op)));
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
                        cars = cars.OrderByDescending(p => p.InsertDate).ToList();
                        break;
                    case "oldest":
                        cars = cars.OrderBy(p => p.InsertDate).ToList();
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
            var skip = grid.pageNumber * grid.take - grid.take;
            int pageCount = (int)Math.Ceiling((double)count / grid.take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = grid.pageNumber;

            cars = cars.Skip(skip).Take(grid.take).ToList();

            return PartialView(cars);
        }
    }
}