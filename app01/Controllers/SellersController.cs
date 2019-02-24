using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app01.Models;
using app01.Services;
using Microsoft.AspNetCore.Mvc;

namespace app01.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;

        public SellersController(SellerService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var List = _service.FindAll();
            return View(List);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller obj)
        {
            _service.Insert(obj);
            return RedirectToAction(nameof(Index));
        }
    }
}