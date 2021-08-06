using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SazeNegar.Infrastructure;
using SazeNegar.Infrastructure.Repositories;

namespace SazeNegar.Infrastructure.Repositories
{
    public class CarClassRepository : BaseRepository<CarClass, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public CarClassRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<CarClass> GetCarClassById(int id)
        {
            var Cars = _context.Cars.Where(i => i.Id == id).Include(x => x.Brand).SingleOrDefault();
            var Brands = _context.Brands.Where(i => i.Id == Cars.BrandsId).Include(x => x.CarModel).SingleOrDefault();
            var CarModel = _context.CarModels.Where(i => i.Id == Brands.CarModelId).Include(x => x.CarClasses).Where(i => i.CarClasses.Count != 0).SingleOrDefault();
            var carClasses = new List<CarClass>();
            foreach (var carClass in _context.CarClasses.ToList())
            {
                foreach (var carModel in carClass.CarModels.ToList())
                {
                    if (carModel.Id == CarModel.Id)
                        carClasses.Add(carClass);
                }
            }

            return carClasses;
        }
    }
}
