using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app01.Models;

namespace app01.Data
{
    public class SeedingService
    {
        private app01Context _context; 

        public SeedingService (app01Context context)
        {

            _context = context;

        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.SalesRecords.Any() || _context.Seller.Any())
            {
                return;
            }

            Department d1 = new Department(1, "Computadores");
            Department d2 = new Department(2, "Eletronicos");
            Department d3 = new Department(3, "Games");

            Seller s1 = new Seller(1, "bob", "1", new DateTime(1998, 1,1), 1000.0, d1);
            Seller s2 = new Seller(2, "bob2", "2", new DateTime(1998, 1,1), 1500.0, d2);
            Seller s3 = new Seller(3, "bob3", "3", new DateTime(1998, 1,1), 1300.0, d3);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 9, 25), 11111.0, Models.Enums.SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2018, 9, 25), 11111.0, Models.Enums.SaleStatus.Billed, s2);

            _context.Department.AddRange(d1, d2, d3);
            _context.Seller.AddRange(s1,s2,s3);
            _context.SalesRecords.AddRange(r1, r2);

            _context.SaveChanges();
        }

    }
}
