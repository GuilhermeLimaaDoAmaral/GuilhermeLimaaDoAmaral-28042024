using ManagementSystem.Entities;

namespace ManagementSystem.Interface
{
    public interface IUserRepository
    {
        long Save(User User);
        List<User> GetAll();
        List<User> GetAllActive();
        void DeleteUserById(int userId);
        User GetUserById(int userId);
        void Update(User User);
        List<User> GetAllInactive();
        bool ExistUser(string userName, string password);
        User GetUserByUserName(string userName);
    }
}
