using app01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app01.Services
{
    public class DepartmentService
    {
        private readonly app01Context _context;

        public DepartmentService(app01Context context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x=> x.Name).ToList();
        }
    }
}
