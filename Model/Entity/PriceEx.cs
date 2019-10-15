using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class PriceEx
    {
        public string ID { set; get; }
        public decimal Price { set; get; }
        public decimal MemberPrice { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
        public string PCode { set; get; }
        public string PName { set; get; }
        public string TypeName { set; get; }
        public string TypeCode { set; get; }
        public string ProductID { set; get; }
        public DateTime CreateDate { set; get; }
        public string UnitID { set; get; }
        public int Amount { set; get; }

    }
}
