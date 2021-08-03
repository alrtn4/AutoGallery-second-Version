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
                carList[i].CarClasses = _carClassRepository.GetCarClassById(item.Id);
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
        
    }
}
