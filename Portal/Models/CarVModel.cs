using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Models
{
    public class CarVModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public CarVModel(Car model)
        {
            if (model == null)
                return;
            CarName = model.Name;
            CarCode = model.Code;
            CarUser = model.Operator.Name;
            UserName = model.Operator.UserName;
            BankName = model.BankName;
            BankId = model.BankId;
            UserPhone = model.Operator.Phone;
            UserPwd = model.Operator.PassWord;
            UserId = model.Operator.Id;
            Id = model.Id;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarName { set; get; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarCode { set; get; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { set; get; }
        /// <summary>
        /// 银行id
        /// </summary>
        public string BankId { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string CarUser { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string UserPhone { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string UserPwd { set; get; }
        /// <summary>
        /// key
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// key
        /// </summary>
        public string UserId { set; get; }
    }
}