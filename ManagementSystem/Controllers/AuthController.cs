using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using ManagementSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class AuthController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly ILoginService _loginService;

        public AuthController(IUserRepository userRepository,ILoginService loginService)
        {
            _userRepository = userRepository;
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(template: "login")]
        public async Task<ActionResult<dynamic>> AutenticateAsync([FromBody] User modelUser)
        {
            if (!_userRepository.ExistUser(modelUser.Username, modelUser.Password))
            {
                return NotFound(new { messege = "Usuario não Encontrado ou inativo." });
            }
            var user = _userRepository.GetUserByUserName(modelUser.Username);
            var token = _loginService.GenerateToken(modelUser.Username);
            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
    }
}
