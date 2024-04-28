using ManagementSystem.Models;

namespace ManagementSystem.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; } // Código único do colaborador
        public string Name { get; set; } // Nome do colaborador
        public int UnitId { get; set; } // ID da unidade à qual o colaborador está associado
        public Unit Unit { get; set; } // Referência à unidade à qual o colaborador está associado
        public int UserId { get; set; } // ID do usuário relacionado
        public User User { get; set; } // Referência ao usuário relacionado

        public static Employee MapEmployee(ModelEmployee modelEmployee)
        {
            return new Employee
            {
                EmployeeId = modelEmployee.EmployeeId,
                Name = modelEmployee.Name,
                UnitId = modelEmployee.UnitId,
                Unit = modelEmployee.Unit,
                UserId = modelEmployee.UserId,
                User = modelEmployee.User
            };
        }

    }

}
