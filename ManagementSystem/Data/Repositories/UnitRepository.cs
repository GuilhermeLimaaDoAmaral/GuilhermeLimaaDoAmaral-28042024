using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagementSystem.Data.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmployeeService _employeeService;
        public UnitRepository(ApplicationDbContext ApplicationDbContext, IEmployeeService employeeService)
        {
            _applicationDbContext = ApplicationDbContext;
            _employeeService = employeeService;
        }

        public int Save(Unit unit)
        {
            if (_applicationDbContext.Unit.Any(u => u.Name == unit.Name))
                throw new ArgumentException("Já existe uma unidade com o mesmo nome.");

            _applicationDbContext.Unit.Add(unit);
            _applicationDbContext.SaveChanges();
            return unit.UnitId;
        }

        public Unit GetByUnitId(int unitId)
        {
            return _applicationDbContext.Unit.FirstOrDefault(u => u.UnitId == unitId);
        }

        public List<Unit> GetAll()
        {
            return _applicationDbContext.Unit.ToList();
        }

        public void Update(Unit unit)
        {
            var existingUnit = GetByUnitId(unit.UnitId);
            if (existingUnit != null)
            {
                if(_employeeService.UnitExists(unit.UnitId) && unit.IsActive == false)
                    throw new ArgumentException("Registro não pode ser excluído poís possui vinculo com colaborador.");
                existingUnit.IsActive = unit.IsActive;
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Unidade não encontrada.");
            }
        }

        public void DeleteById(int unitId)
        {
            var unit = GetByUnitId(unitId);
            if (unit != null)
            {
                if(_employeeService.UnitExists(unitId))
                    throw new ArgumentException("Registro não pode ser excluído poís possui vinculo com colaborador.");

                _applicationDbContext.Unit.Remove(unit);
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Unidade não encontrada.");
            }
        }

        public List<Unit> GetAllActive()
        {
            return _applicationDbContext.Unit.Where(u => u.IsActive).ToList();
        }
    }
}
