using ManagementSystem.Models;

namespace ManagementSystem.Entities
{
    public class User
    {
        public int UserId { get; set; } // Código único
        public string Username { get; set; } // Login
        public string Password { get; set; } // Senha
        public bool IsActive { get; set; } // Status (ativo ou inativo)

        public static User MapUserModel(ModelUser ModelUser)
        {
            return new User
            {
                UserId = ModelUser.UserId,
                Username = ModelUser.Username,
                Password = ModelUser.Password,
                IsActive = ModelUser.IsActive,
            };
        }
    }
}
