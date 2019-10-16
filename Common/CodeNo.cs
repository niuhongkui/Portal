using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CodeNo
    {
        public static string Get(CodeType type)
        {
            string code;
            switch (type)
            {
                case CodeType.ProductType:
                    code = "T";
                    break;
                case CodeType.Product:
                    code = "P";
                    break;
                case CodeType.Unit:
                    code = "U";
                    break;
                case CodeType.Order:
                    code = "O";
                    break;
                default:
                    code = "X";
                    break;
            }
            var s = DateTime.Now.ToString("yyyyMMddhhmmss");
            var r = new Random().Next(1000, 9999);
            return code + s + r;

        }
    }

    public enum CodeType
    {
        [Description("商品类型")]
        ProductType,

        [Description("商品")]
        Product,

        [Description("商品单位")]
        Unit,

        [Description("订单")]
        Order,
    }
}
