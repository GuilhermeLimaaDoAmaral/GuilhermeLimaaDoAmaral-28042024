using ManagementSystem.Entities;

namespace ManagementSystem.Interface
{
    public interface IEmployeeRepository
    {
        int Save(Employee employee);
        Employee GetById(int employeeId);
        List<Employee> GetAll();
        void Update(Employee employee);
        void DeleteById(int employeeId);
    }
}
