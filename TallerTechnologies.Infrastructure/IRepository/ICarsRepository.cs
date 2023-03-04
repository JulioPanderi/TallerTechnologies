using TallerTechnologies.Infrastructure.Models;

namespace TallerTechnologies.Infrastructure.Repositories.Interfaces
{
    public interface ICarsRepository
    {
        Car? GetOne(int id);
        IEnumerable<Car> GetAll();
        void Delete(int id);
        Car Update(Car car);
        Car Insert(Car car);
    }
}