using ManagementSystem.Entities;
using System.Collections.Generic;

namespace ManagementSystem.Interface
{
    public interface IUnitRepository
    {
        int Save(Unit unit);
        void Update(Unit unit);
        void DeleteById(int unitId);
        Unit GetByUnitId(int unitId);
        List<Unit> GetAll();
        List<Unit> GetAllActive();
    }
}
