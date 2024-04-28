using ManagementSystem.Data.Repositories;
using ManagementSystem.Entities;
using ManagementSystem.Models;

namespace ManagementSystem.Interface
{
    public interface IUnitService
    {
        int Create(ModelUnit modelInit);
        void Update(ModelUnit modelInit);
        void DeleteById(int unitId);
        Unit GetUnitById(int unitId);
        List<Unit> GetAll();
        List<Unit> GetAllActive();
    }
}
