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

        //public void AddCar(Cars cars)
        //{
        //    cars.PersianAddedDate = cars.InsertDate != null ? new PersianDateTime(cars.InsertDate.Value).ToString() : "-";
        //}
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

        public List<Cars> GetCarsGrid(int? carBrandId, List<string> carOptions = null, string fromPrice = null, string toPrice = null, string searchString = null)
        {
            var cars = new List<Cars>();
            var count = 0;
            if (carBrandId == null || carBrandId == 0)
            {
                if (string.IsNullOrEmpty(searchString))
                {
                    cars = _context.Cars.Include(p => p.Brand).Include(p => p.CarsInfo).Where(p => p.IsDeleted == false).OrderByDescending(p => p.InsertDate).ToList();
                }
                else
                {
                    cars = _context.Cars.Include(p => p.Brand)
                        .Include(p => p.CarsInfo)
                        .Where(p => p.IsDeleted == false && (p.Brand.Brand.Trim().ToLower().Contains(searchString.Trim().ToLower()) || p.Title.Trim().ToLower().Contains(searchString.Trim().ToLower())))
                        .OrderByDescending(p => p.InsertDate).ToList();
                }
            }
            else
            {
                cars = _context.Cars.Include(p => p.Brand).Include(p => p.CarsInfo).Where(p => p.IsDeleted == false && p.Brand.Id == carBrandId).OrderByDescending(p => p.InsertDate).ToList();

                if (string.IsNullOrEmpty(searchString) == false)
                {
                    cars = cars
                        .Where(p => p.IsDeleted == false && (p.Brand.Brand.Trim().ToLower().Contains(searchString.Trim().ToLower()) || p.Title.Trim().ToLower().Contains(searchString.Trim().ToLower())))
                        .OrderByDescending(p => p.InsertDate).ToList();
                }
            }

            if (carOptions != null && carOptions.Any())
            {
                var Gear = carOptions.Contains("Gear");
                var Sunroof = carOptions.Contains("Sunroof");
                var Doors = carOptions.Contains("Doors");
                var Navigation = carOptions.Contains("Navigation");
                var carsFilteredByOption = new List<Cars>();
                foreach (var option in carOptions)
                    carsFilteredByOption.AddRange(
                        cars.Where(p => p.IsDeleted == false && 
                                        (((p.Gear == "اتومات" || p.Gear == "اتوماتیک") && Gear) 
                                         || (p.Sunroof == "دارد" && Sunroof) 
                                         || (p.CarsInfo.Doors == "دودر" && Doors) 
                                         || (p.Navigation == "دارد" && Navigation)))
                            .OrderByDescending(p => p.InsertDate).ToList());
                cars = carsFilteredByOption;
            }

            if (fromPrice != null)
                cars = cars.Where(p => Convert.ToInt64(p.Price) >= Convert.ToInt64(fromPrice)).ToList();

            if (toPrice != null)
                cars = cars.Where(p => Convert.ToInt64(p.Price) <= Convert.ToInt64(toPrice)).ToList();

            return cars;
        }
        #endregion
    }
}
