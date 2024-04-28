using ManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models
{
    public class ModelEmployee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "O campo nome não pode estar em branco.")]
        public string Name { get; set; }
        public int UnitId { get; set; }
        [Required(ErrorMessage = "O campo Unidade não pode estar em branco.")]
        public Unit Unit { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "O usuário não pode estar em branco.")]
        public User User { get; set; }

        public static ModelEmployee MapEmployee(Employee employee)
        {
            return new ModelEmployee
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                UnitId = employee.UnitId,
                Unit = employee.Unit,
                UserId = employee.UserId,             
                User = employee.User
            };
        }

        public static List<ModelEmployee> MapEmployee(List<Employee> employees)
        {
            List<ModelEmployee> mappedEmployees = new List<ModelEmployee>();

            foreach (var employee in employees)
            {
                ModelEmployee mappedUser = new ModelEmployee
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    UnitId = employee.UnitId,
                    Unit = employee.Unit,
                    UserId = employee.UserId,
                    User = employee.User
                };

                mappedEmployees.Add(mappedUser);
            }

            return mappedEmployees;
        }
    }
}
