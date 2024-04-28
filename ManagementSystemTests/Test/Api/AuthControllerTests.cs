using System.Threading.Tasks;
using ManagementSystem.Controllers;
using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace ManagementSystem.Tests.Controllers
{
    [TestClass()]
    public class AuthControllerTests
    {
        [TestMethod()]
        public async Task AuthenticateAsync_Returns_Unauthorized_For_Invalid_User()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.ExistUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var mockLoginService = new Mock<ILoginService>();
            var controller = new AuthController(mockUserRepository.Object, mockLoginService.Object);

            var invalidUser = new User { Username = "invaliduser", Password = "invalidpassword" };

            // Act
            var result = await controller.AutenticateAsync(invalidUser);

            var notFoundResult = (NotFoundObjectResult)result.Result;
            var value = notFoundResult.Value;
            var message = value?.ToString(); 
            var responseMessage = message.Contains("inativo");

            Assert.IsTrue(responseMessage);
        }

        [TestMethod()]
        public async Task AuthenticateAsync_Returns_authorized_For_valid_User()
        {
            // Arrange
            var validUser = new User { Username = "validuser", Password = "validpassword" };
            var userEntity = new User { Username = "validuser", Password = "validpassword" };
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.ExistUser(validUser.Username, validUser.Password)).Returns(true);
            mockUserRepository.Setup(repo => repo.GetUserByUserName(validUser.Username)).Returns(userEntity); ;


            var mockLoginService = new Mock<ILoginService>();
            mockLoginService.Setup(repo => repo.GenerateToken(validUser.Username)).Returns("tokenFaKe");
            var controller = new AuthController(mockUserRepository.Object, mockLoginService.Object);           

            // Act
            var result = await controller.AutenticateAsync(validUser);
            string jsonValue = JsonConvert.SerializeObject(result.Value);
            var responseMessage = jsonValue.Contains("tokenFaKe");

            Assert.IsTrue(responseMessage); 
        }


    }
}