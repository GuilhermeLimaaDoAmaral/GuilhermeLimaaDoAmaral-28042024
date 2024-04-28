using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Services;
using System;
using System.Linq;

namespace ManagementSystem.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmployeeService _employeeService;
        public UserRepository(ApplicationDbContext ApplicationDbContext, IEmployeeService employeeService)
        {
            _applicationDbContext = ApplicationDbContext;
            _employeeService = employeeService; 
        }

        public long Save(User user)
        {          
            if (_applicationDbContext.User.Any(u => u.Username == user.Username))
                throw new ArgumentException("Já existe um usuário com o mesmo nome.", nameof(user.Username));

            _applicationDbContext.User.Add(user);
            _applicationDbContext.SaveChanges();
            return user.UserId;
        }

        public List<User> GetAll()
        {
            return _applicationDbContext.User.ToList();
        }

        public User GetUserById(int userId)
        {
            return _applicationDbContext.User.FirstOrDefault(u => u.UserId == userId);
        }

        public void DeleteUserById(int userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                if (_employeeService.UserExists(userId))
                    throw new ArgumentException("Registro não pode ser excluído poís possui vinculo com colaborador.");

                _applicationDbContext.User.Remove(user);
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Usuário não encontrado.");
            }
        }

        public void Update(User user)
        {
            var existingUser = GetUserById(user.UserId);
            if (existingUser != null)
            {
                if (_employeeService.UserExists(user.UserId) && user.IsActive == false)
                    throw new ArgumentException("Registro não pode ser desativado poís possui vinculo com colaborador.");

                existingUser.IsActive = user.IsActive;
                existingUser.Password = user.Password;

                _applicationDbContext.SaveChanges(); 
            }
            else
            {
                throw new ArgumentException("Usuário não encontrado.");
            }
        }

        public List<User> GetAllActive()
        {
            return _applicationDbContext.User.Where(u => u.IsActive).ToList();
        }

        public List<User> GetAllInactive()
        {
            return _applicationDbContext.User.Where(u => u.IsActive == false).ToList();
        }

        public bool ExistUser(string userName, string password)
        {
            return _applicationDbContext.User.Any(u => u.Username == userName && u.Password == password && u.IsActive == true);
        }

        public User GetUserByUserName(string userName)
        {
            return _applicationDbContext.User.Where(u => u.Username == userName && u.IsActive == true).FirstOrDefault();
        }
    }
}
