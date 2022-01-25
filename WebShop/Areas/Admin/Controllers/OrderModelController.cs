using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderModelController : Controller
    {
        private readonly AppDbContext _context;
        public OrderModelController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = _context.Users.ToList();
            //List<OrderModel> orders = _context.Order.ToList();
            //List<ProductModel> products = _context.Product.ToList();
            //List<ProductOrderModel> productOrder = _context.ProductOrder.ToList();
            return View(await _context.Order.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ProductOrderModel> productOrders = _context.ProductOrder.ToList();
            var productOrder = await _context.ProductOrder.FirstOrDefaultAsync(p => p.OrderId == id);

            List<ProductModel> products = _context.Product.ToList();


















            var product = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == productOrder.ProductId);

            List<OrderModel> orders = _context.Order.ToList();
            var order = await _context.Order.FirstOrDefaultAsync(p => p.OrderId == productOrder.OrderId);

            List<ApplicationUser> users = _context.Users.ToList();
            if (productOrder == null)
            {
                return NotFound();
            }

            return View(productOrder);
        }
    }
}
