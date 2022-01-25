using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class OrderEditModel
    {
        public ApplicationUser User { get; set; }
        public OrderModel Order { get; set; }
        public ProductModel Product { get; set; }
        public ProductOrderModel ProductOrder { get; set; }
    }
    }