using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using ManagementSystem.Service;
using ManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace ManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitService _unitService;
        private readonly IUserService _userService;
        public EmployeeController(IEmployeeService employeeService, IUnitService unitService, IUserService userService)
        {
            _employeeService = employeeService;
            _unitService = unitService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("userSession");

            if (!string.IsNullOrEmpty(session))
            {
                return View(ModelEmployee.MapEmployee(_employeeService.GetAll()));
            }

            return RedirectToAction("Index", "Login");
            
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.UserList = _userService.GetAllActive();
            ViewBag.UnitList = _unitService.GetAllActive(); 
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.DeleteById(id);
                TempData["MessageSucess"] = "Funcionário excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = $"Falha ao criar a Colaborador.{ex.Message}";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.UserList = _userService.GetAllActive();
            ViewBag.UnitList = _unitService.GetAllActive();
            return View(ModelEmployee.MapEmployee(employee));
        }

        [HttpPost]
        public IActionResult Edit(ModelEmployee modelEmployee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.Update(modelEmployee);
                    TempData["MessageSucess"] = "Funcionário atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    TempData["MessageErro"] = $"Falha ao criar a Colaborador.{ex.Message}";
                    return View(modelEmployee);
                }
            }
            else
            {
                return View(modelEmployee);
            }
        }

        [HttpPost]
        public IActionResult Update(ModelEmployee modelEmployee)
        {
            var allActiveUser = _userService.GetAllActive();
            var allActiveUnit = _unitService.GetAllActive();

            if (ModelState["Unit"].ValidationState == ModelValidationState.Invalid)
            {
                if (ModelState["UnitId"].ValidationState == ModelValidationState.Valid)
                {
                    ModelState["Unit"].Errors.Clear();
                    ModelState["Unit"].ValidationState = ModelValidationState.Valid;
                }
                
            }


            if (ModelState["User"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState["User"].Errors.Clear();
                ModelState["User"].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.Update(modelEmployee);
                    TempData["MessageSucess"] = "Colaborador atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ViewBag.UserList = allActiveUser;
                    ViewBag.UnitList = allActiveUnit;
                    TempData["MessageErro"] = $"Falha ao atualizar o Colaborador.{ex.Message}";
                    return View("EditEmployee", modelEmployee);
                }
            }
            else
            {
                ViewBag.UserList = allActiveUser;
                ViewBag.UnitList = allActiveUnit;
                return View("EditEmployee", modelEmployee);
            }
        }

        public IActionResult Create(ModelEmployee modelEmployee)
        {
            var allActiveUser = _userService.GetAllActive();
            var allActiveUnit = _unitService.GetAllActive();

            ClearModelStateErrorsIfValid("UnitId", "Unit");
            ClearModelStateErrorsIfValid("UserId", "User");

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.Create(modelEmployee);
                    TempData["MessageSucess"] = "Colaborador criado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.UserList = allActiveUser;
                    ViewBag.UnitList = allActiveUnit;
                    TempData["MessageErro"] = $"Falha ao criar a Colaborador.{ex.Message}";
                    return View("Register", modelEmployee);
                }
            }
            else
            {
                ViewBag.UserList = allActiveUser;
                ViewBag.UnitList = allActiveUnit;
                return View("Register", modelEmployee);
            }
        }

        private void ClearModelStateErrorsIfValid(string propertyName, string relatedPropertyName)
        {
            if (ModelState[propertyName].ValidationState == ModelValidationState.Valid)
            {
                ModelState[relatedPropertyName].Errors.Clear();
                ModelState[relatedPropertyName].ValidationState = ModelValidationState.Valid;
            }
        }

    }
}
