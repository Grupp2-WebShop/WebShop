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
            return View(await _context.Order.ToListAsync());
        }
        
        public async Task<IActionResult> EditOrder(OrderModel Order)
        {   
            if (Order == null)
            {
                return NotFound();
            }

            List<ApplicationUser> users = _context.Users.ToList();
            var orderModel = await _context.Order.FindAsync(Order.OrderId);
            var productOrder = await _context.ProductOrder.FirstOrDefaultAsync(p => p.OrderId == Order.OrderId);
            if (productOrder == null)
            {
                return NotFound();
            }
            return View(productOrder);
        }
       
        public async Task<IActionResult> EditOrderConfirmed(ProductOrderModel productOrder)
        {
            if (productOrder == null || !ModelState.IsValid)
            {
                return NotFound();
            }

            var user = productOrder.Order.User;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();                
            return RedirectToAction(nameof(Index));
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
        
        public async Task<IActionResult> DeletePart(ProductOrderModel productOrder)
        {
            if (productOrder == null)
            {
                return NotFound();
            }

            List<ProductModel> products = _context.Product.ToList();
            List<OrderModel> orders = _context.Order.ToList();
            List<ApplicationUser> users = _context.Users.ToList();
            var choosenPartOrder = await _context.ProductOrder.FirstOrDefaultAsync(m => 
                m.ProductId == productOrder.ProductId && 
                m.OrderId == productOrder.OrderId);

            if (choosenPartOrder == null)
            {
                return NotFound();
            }

            return View(choosenPartOrder);
        }

        public async Task<IActionResult> DeletePartConfirmed(ProductOrderModel productOrder)
        {
            var choosenPartOrder = await _context.ProductOrder.FirstOrDefaultAsync(m =>
                m.ProductId == productOrder.ProductId &&
                m.OrderId == productOrder.OrderId);

            List<ProductOrderModel> choosenOrders = _context.ProductOrder.Where(p => p.OrderId == productOrder.OrderId).ToList();
            var order = await _context.Order.FirstOrDefaultAsync(m => m.OrderId == productOrder.OrderId);
            if (choosenPartOrder.Quantity > 1)
            {
                choosenPartOrder.Quantity -= 1;
                _context.ProductOrder.Update(choosenPartOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = productOrder.OrderId });
            }
            if (choosenOrders.Count() <= 1)
            {
                _context.Order.Remove(order);
                _context.ProductOrder.Remove(choosenPartOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _context.ProductOrder.Remove(choosenPartOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = productOrder.OrderId });
        }






        public async Task<IActionResult> EditPart(ProductOrderModel productOrder)
        {
            if (productOrder == null)
            {
                return NotFound();
            }

            List<ProductModel> products = _context.Product.ToList();
            List<OrderModel> orders = _context.Order.ToList();
            List<ApplicationUser> users = _context.Users.ToList();
            var choosenPartOrder = await _context.ProductOrder.FirstOrDefaultAsync(m =>
                m.ProductId == productOrder.ProductId &&
                m.OrderId == productOrder.OrderId);

            if (choosenPartOrder == null)
            {
                return NotFound();
            }

            return View(choosenPartOrder);
        }


        [HttpPost]
        public async Task<IActionResult> EditPartConfirmed(string id, ApplicationUser userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ProductModelExists(userModel.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }





    }
}
