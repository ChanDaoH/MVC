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
using System.Collections;
using Newtonsoft.Json.Linq;

namespace Apps.Web.Areas.MIS.Controllers
{
    public class ProfessorOuterController : BaseController
    {
        //业务层注入
        [Dependency]
        public IMIS_ProfessorOuterBLL m_BLL { get; set; }
        public static string _fileName { get; set; }
        public ValidationErrors errors = new ValidationErrors();

        // GET: MIS/Article
        [SupportFilter(ActionName = "Index")]
        public ActionResult Index()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<MIS_ProfessorOuterModel> modelList = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new MIS_ProfessorOuterModel()
                        {
                            Id = r.Id,
                            uid = r.uid,
                            name = r.name,
                            sex = r.sex,
                            position = r.position,
                            department = r.department,
                            mobile = r.mobile,
                            email = r.email,
                            area = r.area,
                            profession = r.profession,
                            office = r.office,
                            stuNumPG = r.stuNumPG,
                            referee = r.referee,
                            location = r.location,
                            Account = r.Account,

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_ProfessorOuterModel model = m_BLL.GetById(id);
            return View(model);
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_ProfessorOuterModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(MIS_ProfessorOuterModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id, "成功", "修改", "MIS_ProfessorOuter");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "修改", "MIS_ProfessorOuter");
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "MIS_ProfessorOuter");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "MIS_ProfessorOuter");
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
        [SupportFilter]
        public JsonResult Create(MIS_ProfessorOuterModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id, "成功", "创建", "MIS_ProfessorOuter");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "创建", "MIS_ProfessorOuter");
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
            string _fileSaveName = "";
            List<MIS_ProfessorOuterModel> modelList = new List<MIS_ProfessorOuterModel>();
            JsonMessage saveResult = SaveFile(ref _fileSaveName);
            return Json(saveResult);
            /*
            bool checkResult = m_BLL.CheckImportData(_fileSaveName, ref modelList, ref errors);
            if(checkResult)   //可以保存
            {
                m_BLL.SaveImportData(modelList);
                return Json(JsonHandler.CreateMessage(1, "保存成功!"));
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, "失败:<br/>"+errors.Error));
            }
            */
        }
        public void SaveData(MIS_ProfessorOuterModel model)
        {
            m_BLL.Create(ref errors, model);
        }
        public JsonResult GetImportData()
        {
            List<MIS_ProfessorOuterModel> modelList = m_BLL.GetImportData(_fileName);
            var json = new
            {
                rows = (from r in modelList
                        select new MIS_ProfessorOuterModel()
                        {
                            Id = r.Id,
                            uid = r.uid,
                            name = r.name,
                            sex = r.sex,
                            position = r.position,
                            department = r.department,
                            mobile = r.mobile,
                            email = r.email,
                            area = r.area,
                            profession = r.profession,
                            office = r.office,
                            stuNumPG = r.stuNumPG,
                            referee = r.referee,
                            location = r.location,
                            Account = r.Account
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonMessage SaveFile(ref string _fileSaveName)
        {
            HttpPostedFileBase file = Request.Files["file"];
            if (file.ContentLength < 0)
            {
                return JsonHandler.CreateMessage(0, Suggestion.Disable);
            }
            string fileSaveName = System.Guid.NewGuid().ToString("N");
            //文件保存路径
            string fileExtension = Path.GetExtension(file.FileName);    //文件扩展名
            if (fileExtension != ".xlsx" && fileExtension != ".xls")
            {
                return JsonHandler.CreateMessage(0, "请上传.xlsx或.xls文件");
            }
            string fileSavePath = HttpRuntime.AppDomainAppPath + "/Uploads/" + fileSaveName + fileExtension;
            _fileSaveName = fileSavePath;
            _fileName = fileSavePath;
            //保存文件,每次写入2MB
            byte[] buffer;
            Stream stream = file.InputStream;//获取输入流
            int bufferSize = 2048;
            long totalLength = stream.Length; //文件总大小
            long writtenSize = 0; //已写入大小
            using (FileStream fs = new FileStream(fileSavePath, FileMode.Create))
            {
                while (writtenSize < totalLength)
                {
                    if (totalLength - writtenSize > bufferSize)
                    {
                        buffer = new byte[bufferSize];
                    }
                    else
                    {
                        buffer = new byte[totalLength - writtenSize];
                    }
                    stream.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, buffer.Length);
                    writtenSize += bufferSize;
                }
            }
            return JsonHandler.CreateMessage(1, Suggestion.Save);
        }
        #endregion
    }
}
