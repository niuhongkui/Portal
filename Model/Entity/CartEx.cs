using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class CartEx
    {
        public decimal Price { set; get; }
        public decimal MPrice { set; get; }
        public decimal OPrice { set; get; }
        public string Url { set; get; }
       
        public string ID { get; set; }
       
        public string ProductID { get; set; }
       
        public string UnitID { get; set; }
       
        public int Amount { get; set; }
       
        public string ProductName { get; set; }
       
        public string UnitName { get; set; }

        public int LimitNum { get; set; }
        public int StoreAmount { get; set; }
    }
}