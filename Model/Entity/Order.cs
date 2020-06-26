using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{


    public class Order
    {
        public List<PriceEx> GoodsData { set; get; } = new List<PriceEx>();
    }

    public class OrderData:order
    {
        public int IsMember { set; get; }
        public List<OrderDetail> Detail { set; get; }=new List<OrderDetail>();
    }

    public class OrderDetail
    {
        public string Url { set; get; }
        
        public string ID { get; set; }
        
        public string OrderNo { get; set; }
        
        public string ProductID { get; set; }
        
        public string ProductName { get; set; }
        
        public string UnitID { get; set; }
        
        public string UnitName { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal Money { get; set; }
        
        public decimal PMoney { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal PPrice { get; set; }
    }


}
