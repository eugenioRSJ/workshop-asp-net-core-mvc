﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app01.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} required")] // campo obrigatorio 
        [StringLength(60, MinimumLength = 3, ErrorMessage ="O {0} deve conter entre 3 e 60 caracters")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Entre com um email valido")]
        public string Email { get; set; }

        [Display (Name = "Brith Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BrithDate { get; set; }

        [Display(Name = "Base Salary")]//troca o nome
        [DisplayFormat(DataFormatString = "{0:F2}")]//com dois zeros
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}") ] //tamanho de numero
        public double BaseSalary { get; set; } 

        public Department Department    { get; set; }
        public int DepartmentId  { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime brithDate, double baseSalary, Department departa)
        {
            Id = id;
            Name = name;
            Email = email;
            BrithDate = brithDate;
            BaseSalary = baseSalary;
            Department = departa;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales (DateTime initial , DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }

    }
}
