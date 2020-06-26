using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Good
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
        public List<Img> ImgList { set; get; } = new List<Img>();
        /// <summary>
        /// 销量
        /// </summary>
        public int StoreAmount { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 规格
        /// </summary>
        public List<Spec> SpecList { set; get; } = new List<Spec>();

        public string Detail { set; get; }
    }

    public class Spec
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public decimal MPrice { set; get; }
        public int StoreAmount { set; get; }
    }

    public class Img
    {
        public string Url { set; get; }
        
    }
}
