using Microsoft.AspNetCore.Authorization;
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
            return PartialView("_partialCart", cartProducts);
        }

        private ApplicationUser GetUserDetails()
        {
            ApplicationUser[]   user = _context.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToArray();
            return user[0];

        }


        [Authorize]
        [HttpGet]
        public IActionResult ProceedToPayment()
        {
            ProductOrderViewModel proceedToPayment = new ProductOrderViewModel
            { ListCartProduct = cartProducts };
            proceedToPayment.NewOrder = new OrderModel()
            {
                Date = DateTime.Now,
                User = GetUserDetails(),
            };

            proceedToPayment.UserDetails = GetUserDetails();
            return PartialView("_ProceedOrderPayment", proceedToPayment);

        }

        [Authorize]
        [HttpGet]
        public IActionResult NewConfirmedOrder()
        {
            ProductOrderViewModel confirmedOrder = new ProductOrderViewModel
            { ListCartProduct = cartProducts };
            confirmedOrder.NewOrder = new OrderModel()
            {
                Date = DateTime.Now,
                User = GetUserDetails(),                
            };
            confirmedOrder.UserDetails = GetUserDetails();
            try
            {
                _context.Order.Add(confirmedOrder.NewOrder);
                _context.SaveChanges();

                foreach (var product in confirmedOrder.ListCartProduct.GroupBy(p => p.ProductId))
                {
                    _context.ProductOrder.Add(newProductOrder(confirmedOrder.NewOrder, product.First(), product.Count()));
                    _context.SaveChanges();
                }
                return PartialView("_OrderReceipt", confirmedOrder);
            }
            catch
            {
                return NotFound();
            }
        }
        public ProductOrderModel newProductOrder(OrderModel order, ProductModel product , int quantity)
        {
            ProductOrderModel addProductOrder = new ProductOrderModel
            {
                Order = order,
                Product = product,
                Quantity = quantity,
            };            

            return addProductOrder;
        }
    
        [Authorize]
        [HttpGet]
        public IActionResult BacktoCart()
        {
            return RedirectToAction("GetCarttInfo");
        }

        [HttpGet]
        public IActionResult CartSummary()
        {
            
            return PartialView("_partialCartSummary", cartProducts);
        }

        public IActionResult RemoveFromCart(int productId)
        {            
            foreach (var group in cartProducts.GroupBy(p => p.ProductId))

            { 
                if (group.First().ProductId==productId)
                cartProducts.Remove(group.First()); 
            }
                
            //update viewmodel
            ProductViewModel listProductViewModel = new ProductViewModel
            {
                ListProductView = _context.Product.ToList(),
                ListCart = cartProducts
            };
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
