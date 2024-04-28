using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementSystem.Interface
{
    public interface ILoginService
    {
        string GenerateToken(string username);
        bool ValidateToken(string token);

    }
}
