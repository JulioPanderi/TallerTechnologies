using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TallerTechnologies.Infrastructure.Repositories.Interfaces;
using TallerTechnologies.Infrastructure.Models;

namespace TallerTechnologies.Infrastructure.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        AppDbContext _appDbContext;

        public CarsRepository(AppDbContext appDbContext)
        {
            //_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _appDbContext = InitializeMockedDBContext();
        }

        public CarsRepository()
        {
            _appDbContext = InitializeMockedDBContext();
        }

        public IEnumerable<Car> GetAll()
        {
            IEnumerable<Car> retvalue = (from car in _appDbContext.Cars
                                         orderby car.Year, car.Make, car.Model, car.Price
                                         select car
                                        ).AsNoTracking();
            return retvalue;
        }

        public Car? GetOne(int id)
        {
            Car? retvalue = (from car in _appDbContext.Cars
                             where car.Id == id
                             select car
                            ).AsNoTracking().FirstOrDefault();
            return retvalue;
        }

        public void Delete(int id)
        {
            try
            {
                Car car = GetOne(id);
                if (car != null)
                {
                    _appDbContext.ChangeTracker.Clear();
                    _appDbContext.Cars.Remove(car);
                    _appDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Car Update(Car newCar)
        {
            Car? oldCar = GetOne(newCar.Id);
            if (oldCar != null)
            {
                _appDbContext.ChangeTracker.Clear();
                _appDbContext.Cars.Update(newCar);
                _appDbContext.SaveChanges();
                return GetOne(newCar.Id);
            }
            throw new Exception("Car not found");
        }

        public Car Insert(Car newCar)
        {
            newCar.Id = (from car in _appDbContext.Cars
                         orderby car.Id descending
                         select car.Id).FirstOrDefault();
            newCar.Id++;
            _appDbContext.ChangeTracker.Clear();
            _appDbContext.Cars.Add(newCar);
            _appDbContext.SaveChanges();
            return GetOne(newCar.Id);
        }

        protected IEnumerable<Car> GetMockedCarList()
        {
            return new List<Car>() {
                new Car { Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995 },
                new Car { Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995 },
                new Car { Id = 3, Make = "Porsche", Model = "911 991", Year = 2020, Doors = 2, Color = "White", Price = 155000 },
                new Car { Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue", Price = 83995 },
                new Car { Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995 },
                new Car { Id = 6, Make = "Peugeot", Model = "504", Year = 1980, Doors = 4, Color = "Black", Price = 10400 }
            };
        }

        protected AppDbContext InitializeMockedDBContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var context = new AppDbContext(options);
            context.ChangeTracker.LazyLoadingEnabled = false;
            context.ChangeTracker.AutoDetectChangesEnabled = false;

            context.Cars.AddRange(GetMockedCarList());
            context.SaveChanges();

            return context;
        }
    }
}
