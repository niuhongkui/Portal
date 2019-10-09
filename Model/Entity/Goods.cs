using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Entity
{
    public class Goods
    {
        public string image { set; get; }
        public string image2 { set; get; }
        public string image3 { set; get; }

        public string title { set; get; }

        public decimal price { set; get; }

        public decimal sales { set; get; }
        public string ID { set; get; }
    }
}