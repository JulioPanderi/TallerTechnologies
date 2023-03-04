using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerTechnologies.Infrastructure.Repositories.Interfaces;
using TallerTechnologies.Domain.Models;
using AutoMapper;
using TallerTechnologies.Domain.Services.Interfaces;
using TallerTechnologies.Domain.AutoMapper.Profiles;
using TallerTechnologies.Infrastructure.Repositories;

namespace TallerTechnologies.Domain.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IServiceScope _scope;
        //private readonly ILogger<CarService> logger;
        private bool disposedValue;

        public CarsService(
            ICarsRepository repository,
            IServiceScopeFactory serviceScopeFactory)
        {
            _repository = repository;
            //_mapper = mapper;
            _scope = serviceScopeFactory.CreateScope();

            MapperConfiguration configCars = new MapperConfiguration(cfg => {
                cfg.AddProfile(new CarProfile());
            });
            _mapper = new Mapper(configCars);
        }

        public List<CarViewModel> GetAll()
        {
            var cars = _repository.GetAll();
            List<CarViewModel> retValue = (from car in cars
                                           select _mapper.Map<CarViewModel>(car)
                                          ).ToList();
            return retValue;
        }

        public CarViewModel GetOne(int id)
        {
            var car = _repository.GetOne(id);
            return _mapper.Map<CarViewModel>(car);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CarViewModel Update(CarViewModel newCar)
        {
            var newCarDTO = _mapper.Map<TallerTechnologies.Infrastructure.Models.Car>(newCar);
            newCarDTO = _repository.Update(newCarDTO);
            return _mapper.Map<CarViewModel>(newCarDTO);
        }

        public CarViewModel Insert(CarViewModel newCar)
        {
            var newCarDTO = _mapper.Map<TallerTechnologies.Infrastructure.Models.Car>(newCar);
            newCarDTO = _repository.Insert(newCarDTO);
            return _mapper.Map<CarViewModel>(newCarDTO);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) _scope.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}