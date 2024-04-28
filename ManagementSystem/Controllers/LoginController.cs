using ManagementSystem.Data.Repositories;
using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ManagementSystem.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IUserService userService, ILoginService loginService, IHttpClientFactory httpClientFactory)
        {
            _userService = userService;
            _loginService = loginService;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("userSession") != null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userSession");
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Create(ModelUser modelUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    modelUser.IsActive = true;
                    _userService.Create(modelUser);
                    return View("Index"); 
                }

                return View("Register");
            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = $"Falha ao criar usuário. {ex.Message}";
                return View("Register");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(ModelUser modelUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = _httpClientFactory.CreateClient();
                    client.BaseAddress = new Uri("https://localhost:7015/v1/"); 

                    var json = JsonConvert.SerializeObject(modelUser);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic responseData = JsonConvert.DeserializeObject(responseContent);

                        string jwtToken = responseData.token; 

                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            
                        };

                        var userSession = JsonConvert.SerializeObject(modelUser);
                        Response.Cookies.Append("jwt", jwtToken, cookieOptions);
                        HttpContext.Session.SetString("userSession", userSession);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["MessageErro"] = "Falha ao logar no sistema. Usuário não encontrado ou inativo.";
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = $"Falha ao logar no sistema. {ex.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}

