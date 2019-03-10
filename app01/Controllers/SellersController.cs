using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using app01.Models;
using app01.Models.ViewModels;
using app01.Services;
using app01.Services.Exceptions;
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
        public async Task<IActionResult> Index()
        {
            var List = await _service.FindAllAsync();
            return View(List);
        }

        public async Task<IActionResult> Create()
        {

            var Derpatments = await _departmentService.FindAllAsync();
            var ViewModel = new SellerFormViewModel { Departments = Derpatments };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var depataments = await _departmentService.FindAllAsync();
                var ViewModel = new SellerFormViewModel { Seller = seller, Departments = depataments };
                return View(ViewModel);
            }
            await _service.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Provided"});
            }

            var obj = await _service.FindByIdAsync(Id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not Found" });
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _service.FindByIdAsync(Id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not Found" });
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _service.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel
            { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var depataments = await _departmentService.FindAllAsync();
                var ViewModel = new SellerFormViewModel { Seller = seller, Departments = depataments };
                 return View(ViewModel);
            }

            if (Id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _service.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public IActionResult Error (string message)
        {
            var ViewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(ViewModel);
        }
    }
}