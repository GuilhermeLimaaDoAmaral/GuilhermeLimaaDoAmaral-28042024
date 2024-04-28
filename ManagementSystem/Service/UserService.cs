using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using System.Text.RegularExpressions;

namespace ManagementSystem.Service
{
    public class UserService : IUserService
    {
        private readonly bool _requireLowercase = true;
        private readonly bool _requireUppercase = true;
        private readonly bool _requireDigit = true;
        private readonly bool _requireSpecialChar = true;

        private IUserRepository _userRepository { get; set; }
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public long Create(ModelUser modelUser)
        {
            ValidatePassword(modelUser.Password);
            modelUser.UserId = GetNextAvailableUserId();

            return _userRepository.Save(User.MapUserModel(modelUser));
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("A senha deve ter pelo menos 8 caracteres.");

            if (!password.Any(char.IsLower))
                throw new ArgumentException("A senha deve conter pelo menos uma letra minúscula.");

            if (!password.Any(char.IsUpper))
                throw new ArgumentException("A senha deve conter pelo menos uma letra maiúscula.");

            if (!password.Any(char.IsDigit))
                throw new ArgumentException("A senha deve conter pelo menos um dígito.");

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                throw new ArgumentException("A senha deve conter pelo menos um caractere especial.");
        }

        public int GetNextAvailableUserId()
        {
            var allUserIds = _userRepository.GetAll().OrderBy(u => u.UserId).Select(u => u.UserId).ToList();
            var nextAvailableId = 1;

            foreach (var userId in allUserIds)
            {
                if (userId != nextAvailableId)
                {
                    return nextAvailableId;
                }

                nextAvailableId++;
            }

            return nextAvailableId;
        }


        public void Update(ModelUser modelUser)
        {
            _userRepository.Update(User.MapUserModel(modelUser));
        }

        public void DeleteById(int userId)
        {
            _userRepository.DeleteUserById(userId);
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
        public List<User> GetAll()
        {
           return _userRepository.GetAll();
        }

        public List<User> GetAllActive()
        {
            return _userRepository.GetAllActive();
        }
        public List<User> GetAllInactive()
        {
            return _userRepository.GetAllInactive();
        }

        public bool ExistUser(string userName, string password)
        {
            return _userRepository.ExistUser(userName, password);
        }
    }
}

