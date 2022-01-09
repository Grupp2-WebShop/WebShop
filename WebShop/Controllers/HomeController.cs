using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        { _context = context; }
        public IActionResult Index()
        {
            ProductViewModel listProductViewModel = new ProductViewModel { ListProductView = _context.Product.ToList()};
            //Error message ?
            //if (listProductViewModel.ListProductView.Count == 0 || listProductViewModel.ListProductView == null)
            //{
                
            //}
            return View(listProductViewModel);
        }
        [HttpPost]
        public IActionResult Index(ProductViewModel productModel)
        {
            if (productModel.Filter=="" || productModel.Filter == null)
            { productModel.ListProductView = _context.Product.ToList(); }
            else
            { 
            productModel.ListProductView.Clear();

            foreach (var p in _context.Product.ToList())
            {
                if (p.ProductName.Contains(productModel.Filter, StringComparison.OrdinalIgnoreCase))
                {
                    productModel.ListProductView.Add(p);

                }

            }
        }
            return View(productModel);
        }
    }
}
