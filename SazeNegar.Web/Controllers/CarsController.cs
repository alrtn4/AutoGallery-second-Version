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
        [Route("CarListGrid")]
        public ActionResult CarListGrid(GridViewModel grid)
        {
            var cars = new List<Cars>();

            var brandsIntArr = new List<int>();

            if (string.IsNullOrEmpty(grid.brands) == false)
            {
                var brandsArr = grid.brands.Split('-').ToList();
                brandsArr.ForEach(b => brandsIntArr.Add(Convert.ToInt32(b)));
            }

            var subFeaturesIntArr = new List<int>();
            if (string.IsNullOrEmpty(grid.subFeatures) == false)
            {
                var subFeaturesArr = grid.subFeatures.Split('-').ToList();
                subFeaturesArr.ForEach(b => subFeaturesIntArr.Add(Convert.ToInt32(b)));
            }

            cars = _carsRepo.GetCarsGrid(grid.categoryId, brandsIntArr, grid.priceFrom, grid.priceTo, grid.searchString);

            #region Get Products Base on Group, Brand and Products of "offer"

            var allSearchedTargetProducts = new List<Cars>();

            if (grid.GroupIds != null || grid.ProductIds != null || grid.BrandIds != null)
            {
                //search based on multiple group ids
                //if (string.IsNullOrEmpty(grid.GroupIds) == false)
                //{
                //    var groupIdsIntArr = new List<int>();

                //    var groupIdsArr = grid.GroupIds.Split('-').ToList();
                //    groupIdsArr.ForEach(g => groupIdsIntArr.Add(Convert.ToInt32(g)));

                //    var allTargetGroups = new List<ProductGroup>();

                //    foreach (var id in groupIdsIntArr)
                //    {
                //        var group = _productGroupRepo.GetProductGroup(id);

                //        allTargetGroups.Add(group);
                //    }

                //    foreach (var group in allTargetGroups)
                //    {
                //        if (group != null)
                //        {
                //            var allProductsOfOneGroup = _carsRepo.getProductsByGroupId(group.Id);

                //            foreach (var product in allProductsOfOneGroup)
                //            {
                //                allSearchedTargetProducts.Add(product);
                //            }
                //        }
                //    }
                //}

                //search based on multiple brand ids
                if (string.IsNullOrEmpty(grid.BrandIds) == false)
                {
                    var brandIdsIntArr = new List<int>();

                    var brandIdsArr = grid.BrandIds.Split('-').ToList();
                    brandIdsArr.ForEach(b => brandIdsIntArr.Add(Convert.ToInt32(b)));

                    var allTargetBrands = new List<Brands>();

                    foreach (var id in brandIdsIntArr)
                    {
                        var brand = _brandsRepo.GetBrand(id);

                        allTargetBrands.Add(brand);
                    }

                    foreach (var brand in allTargetBrands)
                    {
                        if (brand != null)
                        {
                            var allProductsOfOneBrand = _carsRepo.getProductsByBrandId(brand.Id);

                            foreach (var product in allProductsOfOneBrand)
                            {
                                allSearchedTargetProducts.Add(product);
                            }
                        }
                    }
                }

                //search based on multiple product ids
                if (string.IsNullOrEmpty(grid.ProductIds) == false)
                {
                    var productIdsIntArr = new List<int>();

                    var productIdsArr = grid.ProductIds.Split('-').ToList();
                    productIdsArr.ForEach(b => productIdsIntArr.Add(Convert.ToInt32(b)));

                    foreach (var id in productIdsIntArr)
                    {
                        var product = _carsRepo.GetCar(id);

                        //if product not found in allSearchedTargetProducts
                        if (!allSearchedTargetProducts.Contains(product))
                        {
                            allSearchedTargetProducts.Add(product);
                        }
                    }
                }

                cars = allSearchedTargetProducts;
            }

            #endregion

            #region Sorting

            if (grid.sort != "date")
            {
                switch (grid.sort)
                {
                    case "name":
                        cars = cars.OrderBy(p => p.Title).ToList();
                        break;
                    case "sale":
                        cars = cars.OrderByDescending(p => _productService.GetProductSoldCount(p)).ToList();
                        break;
                    case "price-high-to-low":
                        cars = cars.OrderByDescending(p => _productService.GetProductPriceAfterDiscount(p)).ToList();
                        break;
                    case "price-low-to-high":
                        cars = cars.OrderBy(p => _productService.GetProductPriceAfterDiscount(p)).ToList();
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

            var vm = new List<ProductWithPriceDto>();
            foreach (var product in cars)
                vm.Add(_productService.CreateProductWithPriceDto(product));

            return PartialView(vm);
        }
    }
}