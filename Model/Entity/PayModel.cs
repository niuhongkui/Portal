using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class PayModel
    {
        public string Type { set; get; }
        public string OrderNo { set; get; }
        public decimal Money { set; get; }
        public decimal PMoney { set; get; }
    }

    public class OutPayModel
    {

        public decimal TotalAmount { set; get; }
        public string TimeoutExpress { set; get; }
        public string OutTradeNo { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }

    }
}
