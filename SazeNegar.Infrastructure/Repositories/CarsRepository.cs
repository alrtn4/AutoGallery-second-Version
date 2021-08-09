using SazeNegar.Core.Models;
using SazeNegar.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SazeNegar.Infrastructure.Repositories
{
    public class CarsRepository : BaseRepository<Cars, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        private readonly CarClassRepository _carClassRepository;
        public CarsRepository(MyDbContext context, LogsRepository logger, CarClassRepository carClassRepository) : base(context, logger)
        {
            _context = context;
            _logger = logger;
            _carClassRepository = carClassRepository;
        }

        public Cars GetCar(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id);
        }

        public List<Brands> GetBrandsList()
        {
            return _context.Brands.Where(i => i.IsDeleted == false).ToList();
        }

        public List<CarsInfo> GetCarInfoList()
        {
            return _context.CarsInfos.Where(i => i.IsDeleted == false).ToList();
        }

        public IQueryable<CarModel> GetCarClasses(Cars cars)
        {
           var brand = _context.Cars.Where(i => i.Id == cars.Id).Include(x => x.Brand);
           var carModel = _context.CarModels.Where(x => x.Brand == brand).Include(x => x.CarClasses);
           return carModel;
        }
        public void EditCarBrand(int CarId, string brand)
        {
            var cars = GetCar(CarId);
            var brands = cars.Brand;
            brands.Brand = brand;
            //brands.CarId = CarId;
            cars.Brand = brands;
            Update(cars);
        }
        
        public List<Cars> GetCarsList(int skip, int take, int categoryId)
        {
            return _context.Cars.Where(a => a.IsDeleted == false).OrderByDescending(a => a.Id).Skip(skip).Take(take).ToList();
        }
        public List<Cars> GetCarsList(int skip, int take, string searchString)
        {
            if (searchString != null)
            {
                return _context.Cars
                    .Where(a => a.IsDeleted == false && (a.Title.Trim().ToLower().Contains(searchString.Trim().ToLower())))
                    .OrderByDescending(a => a.Id).Skip(skip).Take(take).ToList();
            }
            else
            {
                return _context.Cars
                    .Where(a => a.IsDeleted == false).Include(x => x.Brand)
                    .OrderByDescending(a => a.Id).Skip(skip).Take(take).ToList();
            }
        }
        public List<CarListViewModel> GetCarsList(int skip, int take)
        {

            List<CarListViewModel> carList = new List<CarListViewModel>();
            var selectedCars = _context.Cars.Where(a => a.IsDeleted == false)
                .OrderByDescending(a => a.Id).Skip(skip).Take(take).ToList();
            int i = 0;
            foreach (var item in selectedCars)
            {
                carList.Add(new CarListViewModel());
                carList[i].Cars = item;
                //carList[i].CarClasses = _carClassRepository.GetCarClassById(item.Id);
                i++;
            }

            return carList;
        }
        
        public int GetCarsCount(int? categoryId = null)
        {
            if (categoryId == null)
                return _context.Cars.Count(a => a.IsDeleted == false);
            else
                return _context.Cars
                    .Count(a => a.IsDeleted == false);
        }

        #region Get Products Grid

        public List<Cars> GetCarsGrid(int? carBrandId, List<int> carOptionIds = null, long? fromPrice = null, long? toPrice = null, string searchString = null)
        {
            var cars = new List<Cars>();
            var count = 0;
            if (carBrandId == null || carBrandId == 0)
            {
                if (string.IsNullOrEmpty(searchString))
                {
                    cars = _context.Cars.Include(p => p.ProductMainFeatures).Include(p => p.ProductFeatureValues).Where(p => p.IsDeleted == false).OrderByDescending(p => p.InsertDate).ToList();

                    foreach (var car in cars)
                    {
                        car.ProductMainFeatures = car.ProductMainFeatures.Where(f => f.IsDeleted == false).ToList();
                    }
                }
                else
                {
                    products = _context.Products.Include(p => p.ProductMainFeatures)
                        .Include(p => p.ProductFeatureValues)
                        .Where(p => p.IsDeleted == false && (p.ShortTitle.Trim().ToLower().Contains(searchString.Trim().ToLower()) || p.Title.Trim().ToLower().Contains(searchString.Trim().ToLower())))
                        .OrderByDescending(p => p.InsertDate).ToList();

                    foreach (var product in products)
                    {
                        product.ProductMainFeatures = product.ProductMainFeatures.Where(f => f.IsDeleted == false).ToList();
                    }
                }
            }
            else
            {
                products = _context.Products.Include(p => p.ProductMainFeatures).Include(p => p.ProductFeatureValues).Where(p => p.IsDeleted == false && p.ProductGroupId == carBrandId).OrderByDescending(p => p.InsertDate).ToList();

                foreach (var product in products)
                {
                    product.ProductMainFeatures = product.ProductMainFeatures.Where(f => f.IsDeleted == false).ToList();
                }

                var allChildrenGroups = GetAllChildrenProductGroupIds(carBrandId.Value);
                foreach (var groupId in allChildrenGroups)
                    products.AddRange(_context.Products.Where(p => p.IsDeleted == false && p.ProductGroupId == groupId).OrderByDescending(p => p.InsertDate).ToList());
                if (string.IsNullOrEmpty(searchString) == false)
                {
                    products = products
                        .Where(p => p.IsDeleted == false && (p.ShortTitle.Trim().ToLower().Contains(searchString.Trim().ToLower()) || p.Title.Trim().ToLower().Contains(searchString.Trim().ToLower())))
                        .OrderByDescending(p => p.InsertDate).ToList();

                    foreach (var product in products)
                    {
                        product.ProductMainFeatures = product.ProductMainFeatures.Where(f => f.IsDeleted == false).ToList();
                    }
                }
            }

            if (carOptionIds != null && carOptionIds.Any())
            {
                var productsFilteredByBrand = new List<Product>();
                foreach (var brand in carOptionIds)
                    productsFilteredByBrand.AddRange(products.Where(p => p.IsDeleted == false && p.BrandId == brand).OrderByDescending(p => p.InsertDate).ToList());
                products = productsFilteredByBrand;
            }

            if (fromPrice != null)
                products = products.Where(p => GetProductPriceAfterDiscount(p) >= fromPrice).ToList();

            if (toPrice != null)
                products = products.Where(p => GetProductPriceAfterDiscount(p) <= toPrice).ToList();

            return products;
        }
        #endregion
    }
}
