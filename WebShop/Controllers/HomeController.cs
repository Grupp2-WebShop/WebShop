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
            ProductViewModel listProductViewModel = new ProductViewModel { ListProductView = _context.Product.ToList()};
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
<<<<<<< HEAD

        [HttpGet]
        public IActionResult GetProductInfo(ProductModel productChoosen)
        {
            ProductModel product = new ProductModel();
            List<ProductModel> ListOfProducts = _context.Product.ToList();
            string productInfo = "";
            foreach(ProductModel p in ListOfProducts)
            {
                if (p.ProductId == productChoosen.ProductId)
                {
                    productInfo = p.Description;
                }
            }            
            return PartialView("_partialProductInfo", productInfo);
=======
        
        public IActionResult BuyClicked(int productId)
        {
            cartProducts.Add(productId);
            TempData["shortMessage"]=$"Added to shopping cart";
            return RedirectToAction("Index");
>>>>>>> b223881aeabdfa3423e2e55565c826b270f7b0d0
        }

    }
}
