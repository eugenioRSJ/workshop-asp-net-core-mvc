using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app01.Models;
using app01.Models.ViewModels;
using app01.Services;
using Microsoft.AspNetCore.Mvc;

namespace app01.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService service, DepartmentService departamentService)
        {
            _departmentService = departamentService;
            _service = service;
        }
        public IActionResult Index()
        {
            var List = _service.FindAll();
            return View(List);
        }

        public IActionResult Create()
        {
            var Derpatments = _departmentService.FindAll();
            var ViewModel = new SellerFormViewModel { Departments = Derpatments};
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _service.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}