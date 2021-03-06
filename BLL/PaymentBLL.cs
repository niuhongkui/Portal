﻿using Common;
using DAL;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Model;
using Newtonsoft.Json.Linq;

namespace BLL
{
    public class PaymentBLL
    {
        private OrderDAL odal = new OrderDAL();
        private SysOptionDAL sdal = new SysOptionDAL();
        private MemberDAL mdal = new MemberDAL();

        public ApiMessage<OutPayModel> CkeckData(PayModel pm, CurrentUser user)
        {
            var res = new ApiMessage<OutPayModel>();
            res.Success = false;
            res.Msg = "数据有误";
            var oData = new OutPayModel();
            if (pm.Type == "order")
            {
                var oModel = odal.Get(pm.OrderNo);
                var oModeld = odal.GetOrder(pm.OrderNo).rows;
                if (oModel.Money != pm.Money)
                    return res;

                oData.Subject = "购买商品";
                foreach (var item in oModeld)
                {
                    oData.Body = oData.Body + item.ProductName + "/" + item.UnitName + " ";
                }
                oData.TotalAmount = pm.Money;
                Random rd = new Random();
                oModel.PayOrderNo = oModel.OrderNo + rd.Next(1000, 10000);
                oModel.Update();
                oData.OutTradeNo = oModel.PayOrderNo;

            }
            else
            {
                var sModel = sdal.GetOptByType("member").FirstOrDefault(n => n.ID == pm.OrderNo);
                if (sModel.OptionValue2 != pm.Money.ToString())
                    return res;
                oData.Subject = "购买会员";
                oData.Body = sModel.OptionValue1;
                oData.TotalAmount = Convert.ToDecimal(sModel.OptionValue2);
                var payNo = CodeNo.Get(CodeType.VIP);
                oData.OutTradeNo = payNo;
                var mb = new member();
                mb.Money = oData.TotalAmount;
                mb.ID = Guid.NewGuid().ToString();
                mb.State = "待支付";
                mb.UserID = user.Id;
                mb.ProName = sModel.OptionValue1;

                mb.ProID = sModel.ID;
                mb.CreateDate = DateTime.Now;
                mb.PMoney = 0;
                mb.OrderNo = payNo;
                mb.PayOrderNo = payNo;
                mb.Insert();
            }
            res.Data = oData;
            res.Msg = "";
            res.Success = true;
            LogHelper.WriteLog("支付请求：" + JsonConvert.SerializeObject(res.Data), LogHelper.LogType.Info);
            return res;
        }

        public ApiMessage<object> CkeckWxData( OutPayModel pm, CurrentUser user)
        {
            var res = new ApiMessage<object>();
            res.Success = false;
            res.Msg = "数据有误";
            var requestXml = WeiXinUtil.BuildRequest(pm.OutTradeNo,pm.Subject,pm.TotalAmount,pm.IP);

            var resultXml = WeiXinUtil.Post("https://api.mch.weixin.qq.com/pay/unifiedorder", requestXml);

            var dic = WeiXinUtil.FromXml(resultXml);

            string returnCode;
            dic.TryGetValue("return_code", out returnCode);

            if (returnCode == "SUCCESS")
            {
                var prepay_id = WeiXinUtil.GetValueFromDic<string>(dic, "prepay_id");
                if (!string.IsNullOrEmpty(prepay_id))
                {
                    var payInfo = JsonConvert.DeserializeObject<WeiXinUtil.WxPayModel>(WeiXinUtil.BuildAppPay(prepay_id));
                    var orderinfo = new
                    {
                        payInfo.appid,
                        payInfo.partnerid,
                        payInfo.prepayid,
                        payInfo.package,
                        payInfo.noncestr,
                        payInfo.timestamp,
                        payInfo.sign,
                        code=0,
                        msg= "成功"
                    };
                    res.Success = true;
                    res.Data = orderinfo;
                    res.Msg = "";
                }
                else
                {
                    res.Msg = "支付错误:" + WeiXinUtil.GetValueFromDic<string>(dic, "err_code_des");
                 
                    return res;
                }
            }
            else
            {
                res.Msg = "配置错误";
                return res;
            }
            return res;
        }

        public ApiMessage<string> UpdateOrder(string orderNo,string trade_no)
        {
            var api = new ApiMessage<string>();
            //VIP购买
            if (orderNo.IndexOf('V') > -1)
            {
                if (!mdal.UpdateVip(orderNo,trade_no))
                {
                    api.Success = false;
                }
            }
            else //订单购买
            {
                orderNo = orderNo.Substring(0, orderNo.Length - 4);
                api = odal.UpdateState(orderNo, trade_no);
            }
            return api;
        }
    }
}
