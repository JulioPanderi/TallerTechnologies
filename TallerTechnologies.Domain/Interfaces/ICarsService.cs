using TallerTechnologies.Domain.Models;

namespace TallerTechnologies.Domain.Services.Interfaces
{
    public interface ICarsService : IDisposable
    {
        CarViewModel? GetOne(int id);
        List<CarViewModel> GetAll();
        void Delete(int id);
        CarViewModel Update(CarViewModel car);
        CarViewModel Insert(CarViewModel car);
    }
}