using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CartDetail
    {
        public List<string> ProductIds { get; set; }

        public CartDetail()
        {
            List<string> ProductIds = new List<string>();
        }
    }
}
