using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Web.Areas.TEST.Controllers
{
    public class FileController : BaseController
    {
        // GET: TEST/File
        public ActionResult Index()
        {
            string clientId = GetUserId();
            return View();
        }

        [HttpPost]
        public string UploadFile()
        {
            //获取请求的文件
            HttpPostedFileBase file = Request.Files["file"];
            if (file.ContentLength <= 0)
            {
                return "{\"sucess\":0,\"message\":\"文件大小为0\"}";
            }
            string clientId = GetUserId();
            string fileSaveName = System.Guid.NewGuid().ToString("N");
            //文件保存路径
            string fileSavePath = HttpRuntime.AppDomainAppPath + "/Uploads/" + fileSaveName + Path.GetExtension(file.FileName);

            Stream stream = file.InputStream;
            byte[] buffer;
            //每次上传的字节
            int bufferSize = 4096;
            //总大小
            long totalLength = stream.Length;
            long writterSize = 0;//已上传文件大小

            object cacheObj = new object();
            using (FileStream fs = new FileStream(fileSavePath, FileMode.Create))
            {
                while (writterSize < totalLength)
                {
                    if (totalLength - writterSize >= bufferSize)
                    {
                        buffer = new byte[bufferSize];
                    }
                    else
                    {
                        buffer = new byte[totalLength - writterSize];
                    }
                    //读出上传文件到字节数组
                    stream.Read(buffer, 0, buffer.Length);
                    //写入文件流
                    fs.Write(buffer, 0, buffer.Length);
                    writterSize += buffer.Length;
                    //将上传百分比写入到服务器缓存中
                    cacheObj = "{\"sucess\":0,\"progress:\""+ (writterSize * 100 / totalLength).ToString() + "\"}";
                    Session["file" + clientId] = cacheObj;
                }
                cacheObj = "{\"success\":1,\"progress\":\"100\"}";
                Session["file" + clientId] = cacheObj;
            }
            return "{\"success\":1,\"progress\":\"100\"}";
        }

        //处理用户询问进度的方法
        public string GetProgress()
        {
            string clientId = GetUserId();
            if (!string.IsNullOrEmpty(clientId))
            {
                object obj = Session["file" + clientId];
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "{\"success\":0,\"progress\":\"0\"}";
                }
            }
            else
            {
                return "{\"success\":0,\"progress\":\"0\"}";
            }
        }
    }
}


    
