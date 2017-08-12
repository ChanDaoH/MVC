using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.Web.Core;
using Apps.MIS.IBLL;
using Apps.Common;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;

namespace Apps.Web.Areas.MIS.Controllers
{
    public class PersonController : BaseController
    {
        //业务层注入
        [Dependency]
        public IMIS_PersonBLL m_BLL { get; set; }

        public ValidationErrors errors = new ValidationErrors();

        // GET: MIS/Article
        [SupportFilter(ActionName = "Index")]
        public ActionResult Index()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<MIS_PersonModel> modelList = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new MIS_PersonModel()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Sex = r.Sex,
                            Age = r.Age,
                            IDCard = r.IDCard,
                            Phone = r.Phone,
                            Email = r.Email,
                            Address = r.Address,
                            CreateTime = r.CreateTime,
                            Region = r.Region,
                            Category = r.Category,

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_PersonModel model = m_BLL.GetById(id);
            return View(model);
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_PersonModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        public JsonResult Edit(MIS_PersonModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id, "成功", "修改", "MIS_Person");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "修改", "MIS_Person");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion
        #region 删除
        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "MIS_Person");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "MIS_Person");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }


        }
        #endregion
        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult Create(MIS_PersonModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id, "成功", "创建", "MIS_Person");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "创建", "MIS_Person");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion
        #region 导入
        public JsonResult Import()
        {
            //获取请求的文件
            HttpPostedFileBase file = Request.Files["file"];
            if(file.ContentLength <= 0)
            {
                return Json(JsonHandler.CreateMessage(0, "文件大小为0"));
            }
            string fileSaveName = System.Guid.NewGuid().ToString("N");
            //文件保存路径
            if(Path.GetExtension(file.FileName) != ".xlsx"&& Path.GetExtension(file.FileName) != ".xls")
            {
                return Json(JsonHandler.CreateMessage(0, "请上传.xlsx文件"));
            }
            string fileSavePath = HttpRuntime.AppDomainAppPath + "/Uploads/" + fileSaveName + Path.GetExtension(file.FileName);
            Stream stream = file.InputStream;   //文件输入流
            byte[] buffer;
            //每次上传的字节
            int bufferSize = 4096;
            //总大小
            long totalLength = stream.Length;
            long writterSize = 0;//已上传文件大小
            using (FileStream fs = new FileStream(fileSavePath, FileMode.Create))
            {
                while(writterSize < totalLength )
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
                }
            }
            var personList = new List<MIS_PersonModel>();

            bool checkResult = m_BLL.CheckImportData(fileSavePath, personList, ref errors);
            if (checkResult)
            {
                m_BLL.SaveImportData(personList);
                LogHandler.WriteServiceLog(GetUserId(), "导入成功", "成功", "导入", "Spl_Person");
                return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), ErrorCol, "失败", "导入", "Spl_Person");
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail +"<br/>"+ ErrorCol));
            }

        }
        #endregion
        #region 导出
        [SupportFilter]
        public ActionResult Export()
        {
            var exportSource = this.GetExportData();
            var js = exportSource.ToString();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSource.ToString());
            var exportFileName = string.Concat(
               "Person",
               DateTime.Now.ToString("yyyyMMddHHmmss"),
               ".xlsx");
            return new ExportExcelResult
            {
                SheetName = "人员列表",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        private JArray GetExportData()
        {
            GridPager pager = new GridPager
            {
                rows = 1000,
                page = 1,
                order = "asc",
                sort = "Id",
            };
            List<MIS_PersonModel> list = m_BLL.GetList(ref pager, "");
            JArray jObjects = new JArray();

            foreach (var item in list)
            {
                var jo = new JObject();
                jo.Add("Id", item.Id);
                jo.Add("Name", item.Name);
                jo.Add("Sex", item.Sex);
                jo.Add("Age", item.Age);
                jo.Add("IDCard", item.IDCard);
                jo.Add("Phone", item.Phone);
                jo.Add("Email", item.Email);
                jo.Add("Address", item.Address);
                jo.Add("CreateTime", item.CreateTime);
                jo.Add("Region", item.Region);
                jo.Add("Category", item.Category);
                jObjects.Add(jo);
            }
            return jObjects;
        }
        #endregion
    }
}
