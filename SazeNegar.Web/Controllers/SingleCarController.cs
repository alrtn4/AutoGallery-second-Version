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
        private readonly GalleriesRepository _galleiesRepo;
        private readonly StaticContentDetailsRepository _contentRepo;

        public SingleCarController(CarsRepository carsRepository, GalleriesRepository galleriesRepository)
        {
            _carsRepo = carsRepository;
            _galleiesRepo = galleriesRepository;
        }

        // GET: Blog
        //[Route("Blog")]
        //[Route("Blog/{id}/{title}")]
        public ActionResult Index(int id)
        {

            CarGalleryViewModel singleCarContent = new CarGalleryViewModel();
            singleCarContent.Cars = _carsRepo.GetCar(id);
            singleCarContent.Galleries = _galleiesRepo.GetAll();
            ViewBag.CarList = _carsRepo.GetCarsList(0, 3);
            return View(singleCarContent);
        }
    }
}