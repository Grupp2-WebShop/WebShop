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

            var productOrder = await _context.ProductOrder.FirstOrDefaultAsync(p => p.OrderId == id);
            if (productOrder == null)
            {
                return NotFound();
            }

            List<ProductModel> products = _context.Product.ToList();
            List<ApplicationUser> users = _context.Users.ToList();
            List<OrderModel> orders = _context.Order.Where(p => p.OrderId == id).ToList();
            List<ProductOrderModel> choosenOrders = _context.ProductOrder.Where(p => p.OrderId == id).ToList();

            return View(choosenOrders);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ProductModel> products = _context.Product.ToList();
            List<ApplicationUser> users = _context.Users.ToList();
            List<OrderModel> orders = _context.Order.Where(p => p.OrderId == id).ToList();
            var choosenOrder = await _context.ProductOrder.FirstOrDefaultAsync(m => m.ProductId == id);
            if (choosenOrder == null)
            {
                return NotFound();
            }

            return View(choosenOrder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.ProductOrder.FindAsync(id);
            _context.ProductOrder.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
