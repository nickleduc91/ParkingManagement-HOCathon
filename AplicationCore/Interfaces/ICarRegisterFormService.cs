using AplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ICarRegisterFormService
    {
        Task addCar(string licensePlateNumber, string ownerId, string model, string make, string colour);
        Task<bool> HasCar(string ownerId);
        Task<Car> getCar(string ownerId);
    }
}