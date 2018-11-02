using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataT1708M1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataT1708M1.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyDbContext _context;
        public ProductController(MyDbContext context)
        {
            _context = context;
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product()
                {
                    Name = "Do An 01",
                    Price = "21000",
                });

                _context.Products.Add(new Product()
                {
                    Name = "Do An 02",
                    Price = "23000",
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int Id)
        {
            var exisProduct = _context.Products.Find(Id);
            if (exisProduct == null)
            {
                return NotFound();
            }
            return View(exisProduct);
        }

        public IActionResult Update(Product product)
        {
            var exisProduct = _context.Products.Find(product.Id);
            if (exisProduct == null)
            {
                return NotFound();
            }
            exisProduct.Name = product.Name;
            exisProduct.Price = product.Price;
            _context.Products.Update(exisProduct);
            _context.SaveChanges();
            TempData["message"] = "Update success";
            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            TempData["message"] = "Insert success";
            return Redirect("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var exisProduct = _context.Products.Find(Id);
            if (exisProduct == null)
            {
                return NotFound();
            }
            _context.Products.Remove(exisProduct);
            _context.SaveChanges();
            return Json(exisProduct);
        }
    }
}