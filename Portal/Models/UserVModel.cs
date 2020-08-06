using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Model;
using Model.Entity;

namespace Portal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserVModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static CurrentUser FormatUser(staff admin)
        {
            CurrentUser user = new CurrentUser();
            user.PassWord = admin.PassWord;
            user.UserName = admin.UserName;
            user.UserCode = admin.UserCode;
            user.UserType = "管理员";
            user.Phone = admin.Phone;
            user.Id = admin.ID;
            user.StationId = admin.StationID;
            user.StationCode = admin.StationCode;
            user.StationName = admin.StationName;
            //foreach (var n in admin.Roles)
            //{
            //    foreach (var l in n.Limits)
            //    {
            //        var limit = new Limit();
            //        limit.ClassName = l.ClassName;
            //        limit.Url = l.Url;
            //        limit.Name = l.Name;
            //        limit.PId = l.PId;
            //        limit.Level = l.Level;
            //        limit.Id = l.Id;

            //        user.Limits.Add(limit);
            //    }
            //}
            return user;
        }
        public static CurrentUser FormatUser(userinfo admin)
        {
            TimeSpan sp = admin.MemberDate.Value.Subtract(DateTime.Now);
            var days = sp.Days;
            CurrentUser user = new CurrentUser();
            user.PassWord = admin.PassWord;
            user.UserName = admin.UserName;
            user.UserCode = admin.UserCode;
            user.ImageUrl = admin.ImgUrl;
            user.UserType = "用户";
            user.Phone = admin.Phone;
            user.Id = admin.ID;
            user.IsMember = days > 0? days :0;
            user.PointAmount = admin.PointAmount.Value;
            return user;
        }
    }
}