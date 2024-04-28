using ManagementSystem.Entities;
using ManagementSystem.Interface;
using ManagementSystem.Models;
using ManagementSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace ManagementSystem.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }
        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("userSession");

            if (!string.IsNullOrEmpty(session))
            {
                return View(ModelUnit.MapUnit(_unitService.GetAll()));
            }

            return RedirectToAction("Index", "Login"); ;          
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
      
        [HttpPost]
        public IActionResult DeleteUnit(int unitId)
        {
            try
            {
                _unitService.DeleteById(unitId);
                TempData["MessageSucess"] = "Unidade excluida com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = $"Falha ao excluir o Unidade: {ex.Message}";
                return View("index", ModelUnit.MapUnit(_unitService.GetAll()));
            }

            return View("index", ModelUnit.MapUnit(_unitService.GetAll()));
        }

        public IActionResult EditUnit(int unitId)
        {
            var unit = _unitService.GetUnitById(unitId);
            if (unit == null)
            {
                return NotFound();
            }

            return View(ModelUnit.MapUnit(unit));
        }

        [HttpPost]
        public IActionResult Update(ModelUnit modelUnit)
        {
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState["Name"].Errors.Clear();
                ModelState["Name"].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitService.Update(modelUnit);
                    TempData["MessageSucess"] = "Unidade atualizada com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    TempData["MessageErro"] = $"Falha ao criar ao atualizar unidade. {ex.Message}";
                    return View("EditUnit", modelUnit);
                }
            }
            else
            {
                return View("EditUnit", modelUnit);
            }
        }

        [HttpPost]
        public IActionResult Create(ModelUnit modelUnit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitService.Create(modelUnit);
                    TempData["MessageSucess"] = "Unidade criada com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["MessageErro"] = $"Falha ao criar a unidade.{ex.Message}";
                    return View(modelUnit);
                }
            }
            else
            {
                return View("Register",modelUnit);
            }
           
        }
    }
}
