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
        private static List<int> cartProducts=new List<int>();
        public HomeController(AppDbContext context)
        { 
            _context = context; 
        }
        public IActionResult Index()
        {
            ProductViewModel listProductViewModel = new ProductViewModel
            {
                ListProductView = _context.Product.ToList(),
                ListCart = _context.Product.Where(p => cartProducts.Contains(p.ProductId)).ToList()
        };
            if (TempData["shortMessage"] != null)
                ViewBag.Message = TempData["shortMessage"].ToString();

            return View(listProductViewModel);
        }
        [HttpPost]
        public IActionResult Index(ProductViewModel productModel)
        {
            if (productModel.Filter=="" || productModel.Filter == null)
            { 
                productModel.ListProductView = _context.Product.ToList(); 
            }
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

        public IActionResult BuyClicked(int productId)
        {

            cartProducts.Add(productId);
            TempData["shortMessage"]=$"Added to shopping cart";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetCarttInfo()
        {
            return PartialView("_partialCart", _context.Product.Where(p => cartProducts.Contains(p.ProductId)).ToList());
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int productId)
        {
            cartProducts.Remove(productId);
            //update viewmodel
            ProductViewModel listProductViewModel = new ProductViewModel
            {
                ListProductView = _context.Product.ToList(),
                ListCart = _context.Product.Where(p => cartProducts.Contains(p.ProductId)).ToList()
            };
            return PartialView("_partialCart", _context.Product.Where(p => cartProducts.Contains(p.ProductId)).ToList());

        }


        [HttpGet]
        public IActionResult GetProductInfo(int productId)
        {
            ProductModel product = new ProductModel();
            product = _context.Product.Find(productId);
            return PartialView("_partialProductInfo", product);
        }
    }
}
