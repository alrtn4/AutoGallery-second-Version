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

        public CarsController(CarsRepository carsRepo, StaticContentDetailsRepository contentRepo)
        {
            _carsRepo = carsRepo;
            _contentRepo = contentRepo;
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
            if (!string.IsNullOrEmpty(null))
            {
                cars = _carsRepo.GetCarsList(skip, take, null);
                count = _carsRepo.GetCarsCount();
                ViewBag.SearchString = null;
                ViewBag.Title = $"جستجو: {null}";
                var pageCount2 = (int)Math.Ceiling((double)count / take);
                ViewBag.PageCount = pageCount2;
                ViewBag.CurrentPage = pageNumber;
                return View(cars);
            }
            count = _carsRepo.GetCarsCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Facebook = _contentRepo.GetStaticContentDetail((int) StaticContents.Facebook).Link;
            ViewBag.Instagram = _contentRepo.GetStaticContentDetail((int)StaticContents.Instagram).Link;
            ViewBag.Twitter = _contentRepo.GetStaticContentDetail((int)StaticContents.Twitter).Link;
            List<IQueryable<CarModel>> carClassList = null;
            
            return View(vm);
        }
        [HttpPost]
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

        public ActionResult Sidebar()
        {
            var sidebarContent = _carsRepo.GetCarsList(0, 3, null);
            return View(sidebarContent);
        }
        public ActionResult Titlebar()
        {
            return View();
        }
    }
}