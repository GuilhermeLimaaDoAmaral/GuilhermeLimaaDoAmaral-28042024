using ManagementSystem.Interface;
using ManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;

namespace ManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("userSession");

            if (!string.IsNullOrEmpty(session))
            {
                return View(ModelUser.MapUser(_userService.GetAll()));
            }

            return RedirectToAction("Index", "Login"); 
        }

        [HttpGet]
        public IActionResult GetUsersByStatus(string status)
        {
            if (status == "ativo")
            {
                return View("Index", ModelUser.MapUser(_userService.GetAllActive()));
            }
            if (status == "inativo")
            {
                return View("Index", ModelUser.MapUser(_userService.GetAllInactive()));
            }
            else
            {
                return View("Index", ModelUser.MapUser(_userService.GetAll()));
            }
        }

        public IActionResult DeleteUser(int userId)
        {
            try
            {
                _userService.DeleteById(userId);
                TempData["MessageSucess"] = $"Usuario excluido com sucesso";
            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = $"Falha ao excluir o usuário: {ex.Message}";
                return View("index", ModelUser.MapUser(_userService.GetAll()));
            }

            return View("index", ModelUser.MapUser(_userService.GetAll()));
        }

        public IActionResult EditUser(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound(); 
            }

            return View(ModelUser.MapUser(user));
        }

        [HttpPost]
        public IActionResult Update(ModelUser modelUser)
        {
            if (ModelState["UserName"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState["UserName"].Errors.Clear();
                ModelState["UserName"].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {               
                try
                {
                    _userService.Update(modelUser);
                    TempData["MessageSucess"] = "Usuário atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    TempData["MessageErro"] = $"Erro ao atualizar usuario. {ex.Message}";
                    return View("EditUser", modelUser);
                }
            }
            else
            {
                return View("EditUser", modelUser);
            }
        }

        [HttpPost]
        public IActionResult Create(ModelUser modelUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.Create(modelUser);
                    TempData["MessageSucess"] = "Usuário criado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    TempData["MessageErro"] = $"Problema ao criar usuario. {ex.Message}";
                    return View("Register", modelUser);
                }
            }
            else
            {
                return View("Register", modelUser);
            }
        }

    }
}
