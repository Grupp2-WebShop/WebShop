using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Areas.User;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Areas.User
{
   [Area("User")]
    public class OrderHistoryController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderHistoryController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewBag.userId = _userManager.GetUserId(HttpContext.User);

            List<ApplicationUser> users = _context.Users.ToList();
            List<OrderModel> orders = _context.Order.ToList();
            List<ProductModel> products = _context.Product.ToList();
            List<ProductOrderModel> productOrder = _context.ProductOrder.ToList();
            List<ProductOrderModel> UserOrders = _context.ProductOrder.Where(o => o.Order.User.Id == userId).ToList();
           
            return View(UserOrders);
            //List<OrderModel> orders = _context.Order.ToList();
            //List<ProductModel> products = _context.Product.ToList();
            //List<ProductOrderModel> productOrder = _context.ProductOrder.ToList();
   
        }

 
















        /*
        private readonly AppDbContext _context;

        public OrderHistoryController(AppDbContext context)
        {
            _context = context;
        }





    public IActionResult Index()
        {

            GetUserController getUserController().Index();

            var userId = UserManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                RedirectToAction("Login", "Account");
            }
            else
            {
                ApplicationUser User = UserManager.FindByIdAsync(userId).Result;
                ViewData["User"] = new ApplicationUser()
                {
                    UserName = User.UserName,
                };


            }

          

            ViewData["Address"] = new OrderModel()
            {
                OrderId = 1,
                User = 
            };


            return View(User);
        }



       /* public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = _context.Users.ToList();
            //List<OrderModel> orders = _context.Order.ToList();
            //List<ProductModel> products = _context.Product.ToList();
            //List<ProductOrderModel> productOrder = _context.ProductOrder.ToList();
            return View(await _context.Order.ToListAsync());
        }*/
    }

}
