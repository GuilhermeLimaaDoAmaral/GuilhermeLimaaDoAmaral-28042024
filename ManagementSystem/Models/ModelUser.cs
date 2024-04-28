using ManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models
{
    public class ModelUser
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "O nome de usuário não pode estar em branco.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "O Senha de usuário não pode estar em branco.")]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public static ModelUser MapUser(User user)
        {
            return new ModelUser
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                IsActive = user.IsActive,
            };
        }

        public static List<ModelUser> MapUser(List<User> users)
        {
            List<ModelUser> mappedUsers = new List<ModelUser>();

            foreach (var user in users)
            {
                ModelUser mappedUser = new ModelUser
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password,
                    IsActive = user.IsActive
                };

                mappedUsers.Add(mappedUser);
            }

            return mappedUsers;
        }
    }
}
