using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;
using Model.Entity;

namespace Portal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BankVModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public BankVModel(Bank model)
        {
            if (model == null)
                return;
            BankName = model.Name;
            BankCode = model.Code;
            BankUser = model.Operator.Name;
            UserName = model.Operator.UserName;
            UserPhone = model.Operator.Phone;
            UserPwd = model.Operator.PassWord;
            UserId = model.Operator.Id;
            Id = model.Id;
        }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { set; get; }
        /// <summary>
        /// 银行编码
        /// </summary>
        public string BankCode { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string BankUser { set; get; }
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Bank ToModel()
        {
            var bank = new Bank();
            bank.Id = Id;
            bank.Code = BankCode;
            bank.Name = BankName;
            bank.IsDelete = false;
            bank.Admin = UserId;
            var opr = new Operator();            
            opr.Name = BankUser;
            opr.UserName = UserName;
            opr.UserType = "1";
            opr.Phone = UserPhone;
            opr.PKId = Id;
            opr.PassWord = UserPwd;
            opr.Id = UserId;
            bank.Operator = opr;
            return bank;
        }

    }
}