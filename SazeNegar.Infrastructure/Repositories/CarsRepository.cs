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

        public void EditCarBrand(int CarId, string brand)
        {
            var cars = GetCar(CarId);
            var brands = cars.Brand;
            brands.Brand = brand;
            //brands.CarId = CarId;
            cars.Brand = brands;
            Update(cars);
        }
    }
}
