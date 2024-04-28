using ManagementSystem.Entities;
using ManagementSystem.Models;

namespace ManagementSystem.Interface
{
    public interface IUserService
    {
        long Create(ModelUser modelUser);
        List<User> GetAll();
        List<User> GetAllActive();
        void DeleteById(int UserId);
        User GetUserById(int UserId);
        bool ExistUser(string userName, string password);
        void Update(ModelUser modelUser);
        List<User> GetAllInactive();

    }
}
