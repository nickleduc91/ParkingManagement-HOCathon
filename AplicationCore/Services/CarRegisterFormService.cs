using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoCoordinatePortable;



namespace ApplicationCore.Services
{
    public class CarRegisterFormService : ICarRegisterFormService 
    {
        private readonly IRepository<Car> _carRepository;
        public CarRegisterFormService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task addCar(string licensePlateNumber, string ownerId, string model, string make, string colour)
        {
            var carSpec = new CarSpecification(ownerId);
            var car = await _carRepository.FirstOrDefaultAsync(carSpec);

            if( car == null)
            {
                car = new Car(licensePlateNumber, ownerId, model, make, colour);
                await _carRepository.AddAsync(car);
                return;
            }

            car.Update(licensePlateNumber, model, make, colour);
            await _carRepository.UpdateAsync(car);


        }

        public async Task<bool> HasCar(string ownerId)
        {
            var carSpec = new CarSpecification(ownerId);
            var car = await _carRepository.FirstOrDefaultAsync(carSpec);

            if (car == null)
            {
                return false;
            }
            
            return true; ;

        }

        public async Task<Car> getCar(string ownerId)
        {
            var carSpec = new CarSpecification(ownerId);
            var car = await _carRepository.FirstOrDefaultAsync(carSpec);
            return car;
        }
    }
}
