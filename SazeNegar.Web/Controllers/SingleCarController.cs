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
    public class SingleCarController : Controller
    {
        private readonly CarsInfoRepository _carsInfoRepo;
        private readonly CarsRepository _carsRepo;
        private readonly GalleriesRepository _galleriesRepo;
        private readonly StaticContentDetailsRepository _contentRepo;

        public SingleCarController(CarsRepository carsRepository, GalleriesRepository galleriesRepository)
        {
            _carsRepo = carsRepository;
            _galleriesRepo = galleriesRepository;
        }

        // GET: Blog
        //[Route("Blog")]
        //[Route("Blog/{id}/{title}")]
        public ActionResult Index(int id)
        {
            var car = _carsRepo.GetCar(id);
            var singleCarVm = new SingleCarViewModel(car);
            singleCarVm.Galleries = _galleriesRepo.GetAll();
            var carList = _carsRepo.GetCarsList(0, 3, null);
            singleCarVm.CarDateList = new List<CarDateViewModel>();
            foreach (var item in carList)
            {
                singleCarVm.CarDateList.Add(new CarDateViewModel(item));
            }
            var cars = _carsRepo.GetCarsList(0, 3, null);
            var carDateList = new List<CarDateViewModel>();
            foreach (var item in cars)
            {
                carDateList.Add(new CarDateViewModel(item));
            }

            singleCarVm.CarDateList = carDateList;
            return View(singleCarVm);
        }
    }
}