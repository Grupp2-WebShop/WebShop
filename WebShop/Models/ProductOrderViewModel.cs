using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ProductOrderViewModel
    {
        public List<ProductModel> ListProduct{ get; set; }
        public ProductOrderViewModel()
        {
            ListProduct = new List<ProductModel>();
        }
    }
}
