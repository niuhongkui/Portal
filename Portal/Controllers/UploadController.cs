using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Portal.Controllers
{
    /// <summary>
    /// 上传
    /// </summary>
    public class UploadController : BaseController
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult Img(HttpPostedFileBase file)
        {
            var res=new ApiMessage<string>();
            string[] limitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
            if (file != null && file.ContentLength > 0)
            {
                var name = file.FileName;
                //获取后缀名
                string namejpg = Path.GetExtension(name).ToUpper();
                //判断是否符合要求
                if (!limitPictureType.Contains(namejpg) && file.ContentLength > 0)
                {
                    res.Success = false;
                    res.Msg = "图片格式错误";
                    return Json(res);
                }
                var tempPath = $"/upload/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                //获取上传的路径
                var path = System.Web.Hosting.HostingEnvironment.MapPath(tempPath);
                //生成一个新的文件名
                var gid = Guid.NewGuid().ToString();
                var newname = gid + namejpg;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //上传
                file.SaveAs(path + newname);
                res.Data = tempPath + newname;
                return Json(res);

            }
            res.Success = false;
            res.Msg = "上传文件有误";
            return Json(res);
        }
    }
}