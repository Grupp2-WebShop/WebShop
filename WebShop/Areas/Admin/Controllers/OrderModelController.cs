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

        public async Task<IActionResult> DeleteOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ApplicationUser> users = _context.Users.ToList();
            var choosenOrder = await _context.Order.FirstOrDefaultAsync(m => m.OrderId == id);
            if (choosenOrder == null)
            {
                return NotFound();
            }

            return View(choosenOrder);
        }

        public async Task<IActionResult> DeleteOrderConfirmed(int id)
        {
            var order = await _context.Order.FirstOrDefaultAsync(m => m.OrderId == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeletePart(int? orderId, int productId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            List<ProductModel> products = _context.Product.ToList();
            List<ApplicationUser> users = _context.Users.ToList();
            List<OrderModel> orders = _context.Order.Where(p => p.OrderId == orderId).ToList();
            List<ProductOrderModel> choosenOrders = _context.ProductOrder.Where(p => p.OrderId == orderId).ToList();

            //var choosenOrder = choosenOrders.Where(p => p.ProductId == orderId);

            var choosenOrder = await _context.ProductOrder.FirstOrDefaultAsync(m => m.ProductId == productId);
            //choosenOrder.Order = choosenOrders.Where(p => p.ProductId == id);

            if (choosenOrder == null)
            {
                return NotFound();
            }

            return View(choosenOrder);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePartConfirmed(int id)
        {
            var order = await _context.ProductOrder.FindAsync(id);
            _context.ProductOrder.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
