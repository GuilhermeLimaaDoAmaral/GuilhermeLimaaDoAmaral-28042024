using ManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models
{
    public class ModelUnit
    {
        public int UnitId { get; set; } 
        public int Code { get; set; }
        [Required(ErrorMessage = "O nome da unidade não pode estar em branco.")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public static ModelUnit MapUnit(Unit unit)
        {
            return new ModelUnit
            {
                UnitId = unit.UnitId,
                Name = unit.Name,
                Code = unit.Code,
                IsActive = unit.IsActive
            };
        }

        public static List<ModelUnit> MapUnit(List<Unit> units)
        {
            List<ModelUnit> mappedUnits = new List<ModelUnit>();

            foreach (var unit in units)
            {
                ModelUnit mappedUser = new ModelUnit
                {
                    UnitId = unit.UnitId,
                    Name = unit.Name,
                    Code = unit.Code,
                    IsActive = unit.IsActive
                };

                mappedUnits.Add(mappedUser);
            }

            return mappedUnits;
        }
    }
}
