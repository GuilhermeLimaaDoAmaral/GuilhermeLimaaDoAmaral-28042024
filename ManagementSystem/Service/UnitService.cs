using ManagementSystem.Data.Repositories;
using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace ManagementSystem.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public int Create(ModelUnit modelInit)
        {

            modelInit.Code = GetNextAvailableUnitId();

            return _unitRepository.Save(Unit.MapModelUnit(modelInit));
        }

        public int GetNextAvailableUnitId()
        {
            var allUnitIds = _unitRepository.GetAll().OrderBy(u => u.UnitId).Select(u => u.UnitId).ToList();
            var nextAvailableId = 1;

            foreach (var unitId in allUnitIds)
            {
                if (unitId != nextAvailableId)
                {
                    return nextAvailableId;
                }

                nextAvailableId++;
            }

            return nextAvailableId;
        }

        public void Update(ModelUnit modelInit)
        {
            _unitRepository.Update(Unit.MapModelUnit(modelInit));
        }

        public void DeleteById(int unitId)
        {
            _unitRepository.DeleteById(unitId);
        }

        public Unit GetUnitById(int unitId)
        {
            return _unitRepository.GetByUnitId(unitId);
        }

        public List<Unit> GetAll()
        {
            return _unitRepository.GetAll();
        }

        public List<Unit> GetAllActive()
        {
            return _unitRepository.GetAllActive();
        }
    }
}
