using ManagementSystem.Entities;
using ManagementSystem.Models;
using System.Collections.Generic;

namespace ManagementSystem.Interface
{
    public interface IEmployeeService
    {
        int Create(ModelEmployee modelEmployee);
        void Update(ModelEmployee modelEmployee);
        void DeleteById(int employeeId);
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAll();
        bool UserExists(int userId);
        bool UnitExists(int userId);
    }
}
