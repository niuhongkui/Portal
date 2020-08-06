using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ApiMessage<T>
    {
        public ApiMessage() {
            Success = true;
            MsgCode = "200";
            Msg = "操作成功";
        }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { set; get; }                
        /// <summary>
        /// 返回状态码，200正确，其余均错误
        /// </summary>
        public string MsgCode { set; get; }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string Msg { set; get; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { set; get; }
    }
}
