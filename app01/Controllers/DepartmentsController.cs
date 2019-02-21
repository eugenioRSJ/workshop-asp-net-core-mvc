using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app01.Models;
using Microsoft.AspNetCore.Mvc;

namespace app01.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name= "Eugênio" });
            list.Add(new Department { Id = 2, Name= "Marcos" });
            return View(list);
        }
    }
}