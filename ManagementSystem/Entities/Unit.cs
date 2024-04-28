using ManagementSystem.Models;

namespace ManagementSystem.Entities
{
    public class Unit
    {
        public int UnitId { get; set; } // ID único da unidade
        public int Code { get; set; } // Código único da unidade
        public string Name { get; set; } // Nome da unidade
        public bool IsActive { get; set; } // Status (ativo ou inativo) da unidade

        public static Unit MapModelUnit(ModelUnit modelInit)
        {
            return new Unit
            {
                UnitId = modelInit.UnitId,
                Name = modelInit.Name,
                Code = modelInit.Code,
                IsActive = modelInit.IsActive
            };
        }
    }
}
