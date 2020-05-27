using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class ProductEx : product
    {
        public List<ProImg> Imgs { set; get; } = new List<ProImg>();
    }

    public class ProImg
    {
        public string name { set; get; }
        public string url { set; get; }
    }
}
