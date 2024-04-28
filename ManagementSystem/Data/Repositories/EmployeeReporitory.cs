using ManagementSystem.Entities;
using ManagementSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagementSystem.Data.Repositories
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public int Save(Employee employee)
        {
            if (_applicationDbContext.Employee.Any(u => u.Name == employee.Name))
                throw new ArgumentException("Já existe um Colaborador com o mesmo nome.", nameof(employee.Name));

            _applicationDbContext.Employee.Add(employee);
            _applicationDbContext.SaveChanges();
            return employee.EmployeeId;
        }

        public Employee GetById(int employeeId)
        {
            return _applicationDbContext.Employee.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public List<Employee> GetAll()
        {
            return _applicationDbContext.Employee.ToList();
        }

        public void Update(Employee employee)
        {
            var existingEmployee = GetById(employee.EmployeeId);
            if (existingEmployee != null)
            {             
                existingEmployee.Name = employee.Name;
                employee.Unit = employee.Unit;
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Funcionário não encontrado.");
            }
        }

        public void DeleteById(int employeeId)
        {
            var employee = GetById(employeeId);
            if (employee != null)
            {
                _applicationDbContext.Employee.Remove(employee);
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Funcionário não encontrado.");
            }
        }
    }
}
