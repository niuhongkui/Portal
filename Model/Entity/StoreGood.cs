using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    /// <summary>
    /// 带库存的所有商品
    /// </summary>
    public class StoreGood
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 商品类别ID
        /// </summary>
        public string TypeID { set; get; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string Url { set; get; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { set; get; }
        /// <summary>
        /// 商品会员价
        /// </summary>
        public decimal MemberPrice { set; get; }
        /// <summary>
        /// 会员价限购
        /// </summary>
        public int LimitNum { set; get; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Amount { set; get; }
        /// <summary>
        /// 商品单位ID
        /// </summary>
        public string UnitID { set; get; }
        /// <summary>
        /// 商品单位名称
        /// </summary>
        public string UnitName { set; get; }

        public int OutAmount { set; get; }

        public int SelectAmount { set; get; }
    }
}
