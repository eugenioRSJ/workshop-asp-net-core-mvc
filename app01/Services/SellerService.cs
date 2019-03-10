using app01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app01.Services.Exceptions;

namespace app01.Services
{
    public class SellerService 
    {
        private readonly app01Context _context;

        public SellerService(app01Context context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Seller.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int Id) 
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == Id);
        }

        public void Remove (int Id)
        {
            var obj = _context.Seller.Find(Id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) //Testa se nao existe algum registro no banco de dados
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
    