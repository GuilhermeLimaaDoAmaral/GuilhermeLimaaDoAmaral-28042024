using ManagementSystem.Data.Repositories;
using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public int Create(ModelEmployee modelEmployee)
        {
            modelEmployee.EmployeeId = GetNextAvailableUnitId();

            return _employeeRepository.Save(Employee.MapEmployee(modelEmployee));
        }


        public int GetNextAvailableUnitId()
        {
            var allEmployeeIdIds = _employeeRepository.GetAll().OrderBy(u => u.EmployeeId).Select(u => u.EmployeeId).ToList();
            var nextAvailableId = 1;

            foreach (var employeeId in allEmployeeIdIds)
            {
                if (employeeId != nextAvailableId)
                {
                    return nextAvailableId;
                }
                nextAvailableId++;
            }
            return nextAvailableId;
        }

        public void Update(ModelEmployee modelEmployee)
        {
            _employeeRepository.Update(Employee.MapEmployee(modelEmployee));
        }

        public void DeleteById(int employeeId)
        {
            _employeeRepository.DeleteById(employeeId);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetById(employeeId);
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }
        public bool UnitExists(int unitId)
        {
            return _employeeRepository.GetAll().Any(e => e.UnitId == unitId);
        }
        public bool UserExists(int userId)
        {
            return _employeeRepository.GetAll().Any(u => u.UserId == userId);
        }
    }
}
