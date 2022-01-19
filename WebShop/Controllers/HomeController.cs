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
        public static List<ProductModel> cartProducts=new List<ProductModel>();
        
        public HomeController(AppDbContext context)
        { 
            _context = context; 
        }

        public IActionResult Index()
        {
            ProductViewModel listProductViewModel = new ProductViewModel
            {
                ListProductView = _context.Product.ToList(),
                ListCart = cartProducts//_context.Product.Where(p => cartProducts.Contains(p.ProductId)).ToList()
            };

            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }
            return View(listProductViewModel);
        }

        [HttpPost]
        public IActionResult Index(ProductViewModel productModel)
        {
            if (productModel.Filter=="" || productModel.Filter == null)
            { 
                productModel.ListProductView = _context.Product.ToList();
                productModel.ListCart = cartProducts;
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
            cartProducts.Add(_context.Product.Find(productId));
            TempData["shortMessage"]=$"Added to shopping cart";
            return RedirectToAction("Index");
        }

        public IActionResult EditClicked(int productId)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCarttInfo()
        {
            ProductViewModel listProductViewModel = new ProductViewModel
            {
                ListProductView = _context.Product.ToList(),
                ListCart = cartProducts
            };
            return PartialView("_partialCartSummary", listProductViewModel);
        }

        [HttpGet]
        public IActionResult ProceedToPayment()
        {
            return PartialView("_ProceedOrderPayment", cartProducts);
        }

        [HttpGet]
        public IActionResult CartSummary()
        {
            ProductViewModel listProductViewModel = new ProductViewModel
            {
                ListProductView = _context.Product.ToList(),
                ListCart = cartProducts//_context.Product.Where(p => cartProducts.Contains(p.ProductId)).ToList()
            };
            return PartialView("_partialCartSummary", cartProducts);
        }

        public IActionResult RemoveFromCart(int productId)
        {            
            foreach (var group in cartProducts.GroupBy(p => p.ProductId))

            { 
                if (group.First().ProductId==productId)
                cartProducts.Remove(group.First());
                break;
            }
                
            return RedirectToAction("Index");
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
