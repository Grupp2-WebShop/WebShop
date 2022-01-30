using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebShop.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;

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
                ListCart = cartProducts
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
            if (productModel.Filter == "" || productModel.Filter == null)
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

        [Authorize]
        public IActionResult BuyClicked(int productId)
        {
            List<string> listCart = new List<string>();
            if (HttpContext.Session.Get("cart") == null)
            {
                listCart.Add(productId.ToString());
                HttpContext.Session.SetString("cart", listCart.ToString());
                ViewBag.cart = listCart.Count();
                HttpContext.Session.SetString("cartCount", listCart.Count().ToString());
            }
            else
            {
                listCart = (List<string>)HttpContext.Session.GetString("cart").Split("").ToList();
                listCart.Add(productId.ToString());
                HttpContext.Session.SetString("cart", listCart.ToString());
                ViewBag.cart = listCart.Count();
                HttpContext.Session.SetString("cartCount", (Convert.ToInt32(HttpContext.Session.GetString("cartCount")) + 1).ToString());
            }

            cartProducts.Add(_context.Product.Find(productId));
            TempData["shortMessage"] = $"Added to shopping cart";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ResetCartProducts()
        {
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Remove("cartCount");
            cartProducts = new List<ProductModel>();
            return RedirectToAction("Index");
        }
        public IActionResult EditClicked(int productId)
        {
            ProductModel product = new ProductModel();
            product = _context.Product.Find(productId);
            return PartialView("_partialEdit", product);
        }

        public IActionResult CreateClicked()
        {
            ProductModel product = new ProductModel();
            return PartialView("_partialCreateProduct", product);
        }

        public IActionResult DeleteClicked(int productId)
        {
            ProductModel product = new ProductModel();
            product = _context.Product.Find(productId);
            return PartialView("_partialDeleteProduct", product);
        }

        [HttpGet]
        public IActionResult GetCarttInfo()
        {
            ProductViewModel listProductViewModel = new ProductViewModel
            {
                ListProductView = _context.Product.ToList(),
                ListCart = cartProducts
            };
            return PartialView("_partialShoppingCart", listProductViewModel);
        }

        private ApplicationUser GetUserDetails()
        {
            ApplicationUser[] user = _context.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToArray();
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
        [HttpPost]
        public FileResult Export(string ReceiptHtml)
        {
            MemoryStream memoryStream = new MemoryStream();            
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(ReceiptHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Receipt.pdf");
            }            
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
                User = GetUserDetails()
            };
            confirmedOrder.UserDetails = GetUserDetails();
            try
            {
                _context.Order.Add(confirmedOrder.NewOrder);
                _context.SaveChanges();

                ProductOrderModel productOrder = new ProductOrderModel();
                foreach (var product in confirmedOrder.ListCartProduct.GroupBy(p => p.ProductId))
                {
                    productOrder.Quantity = product.Count();
                    productOrder.ProductId = product.First().ProductId;
                    productOrder.OrderId = confirmedOrder.NewOrder.OrderId;
                    _context.ProductOrder.Add(productOrder);
                    _context.SaveChanges();
                }

                IActionResult actionResult = ResetCartProducts();
                return PartialView("_OrderReceipt", confirmedOrder);
            }
            catch
            {
                return NotFound();
            }
        }
        public ProductOrderModel newProductOrder(OrderModel order, ProductModel product, int quantity)
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
        [Authorize]
        [HttpGet]
        public IActionResult Proceed()
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CartSummary()
        {
            if (HttpContext.Session.GetString("cartCount") != null)
                ViewData["CartCount"] = HttpContext.Session.GetString("cartCount");

            return PartialView("_partialCartSummary");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            foreach (var group in cartProducts.GroupBy(p => p.ProductId))
            {
                if (group.First().ProductId == productId)cartProducts.Remove(group.First());
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

        // POST: Admin/ProductModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProductId,ProductName,Price,Description,ImageName")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage([Bind("ProductId,ProductName,Price,Description,ImageName")] ProductModel productModel, IFormFile ImageName)
        {
            if (ModelState.IsValid)
            {
                var filename = ContentDispositionHeaderValue.Parse(ImageName.ContentDisposition).FileName.Trim('"');
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", ImageName.FileName);
                using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                {
                    await ImageName.CopyToAsync(stream);
                }
                productModel.ImageName = filename;
                _context.Update(productModel);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private bool ProductModelExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }

        // POST: Admin/ProductModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,Price,Description")] ProductModel productModel, IFormFile ImageName)
        {
            var filename = ContentDispositionHeaderValue.Parse(ImageName.ContentDisposition).FileName.Trim('"');
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", ImageName.FileName);
            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await ImageName.CopyToAsync(stream);
            }
            productModel.ImageName = filename;
            _context.Add(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Admin/ProductModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Admin/ProductModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Product.FindAsync(id);
            _context.Product.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
