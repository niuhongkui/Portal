using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
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
        public static CurrentUser FormatUser(Operator admin)
        {
            CurrentUser user = new CurrentUser();
            user.PassWord = admin.PassWord;
            user.UserName = admin.UserName;
            user.Name = admin.Name;
            user.UserType = admin.UserType;
            user.Phone = admin.Phone;
            user.Id = admin.Id;
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
    }
}