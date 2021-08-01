using SazeNegar.Core.Models;
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
        public CarsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        //public bool CarHasBrand(string carId, string brandId)
        //{
        //    return _context.Cars.Where(a => a.UserId == userId && a.RoleId == roleId).Any();
        //}
        //public List<Brands> GetBrands()
        //{
        //    return _context.Brands.ToList();
        //}

        public Cars GetCar(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id);
        }

        public List<Brands> GetBrandsList()
        {
            return _context.Brands.Where(i => i.IsDeleted == false).ToList();
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
                    .Where(a => a.IsDeleted == false)
                    .OrderByDescending(a => a.Id).Skip(skip).Take(take).ToList();
            }
        }
        //public List<Cars> GetCarsList()
        //{
        //    return _context.Cars.Where(a => a.IsDeleted == false).OrderByDescending(a => a.Id).Skip(skip).Take(take).ToList();
        //}
        public int GetCarsCount(int? categoryId = null)
        {
            if (categoryId == null)
                return _context.Cars.Count(a => a.IsDeleted == false);
            else
                return _context.Cars
                    .Count(a => a.IsDeleted == false);
        }
        //public List<Cars> GetTopCars(int? take = null)
        //{
        //    return take != null ? _context.Articles.Where(a => a.IsDeleted == false).OrderByDescending(a => a.ViewCount).Take(take.Value).ToList() : _context.Articles.OrderByDescending(a => a.ViewCount).ToList();
        //}
    }
}
