using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class OrderController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
