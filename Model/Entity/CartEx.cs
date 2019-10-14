using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class CartEx:cart
    {
        public decimal Price { set; get; }
        public decimal MPrice { set; get; }
    }
}
