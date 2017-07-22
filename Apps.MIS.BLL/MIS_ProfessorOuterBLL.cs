using Apps.Common;
using Apps.Models.Sys;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using Apps.BLL.Core;

namespace Apps.MIS.BLL
{
    public partial class MIS_ProfessorOuterBLL
    {
        public bool CheckImportData(string fileName, ref List<MIS_ProfessorOuterModel> modelList, ref ValidationErrors errors)
        {
            try
            {
                var excel = new ExcelQueryFactory(fileName);
                //映射excel列名
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.uid, "证书编号");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.name, "姓名");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.stuNumPG, "意向人数");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.sex, "性别");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.position, "职称/职务");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.department, "单位");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.mobile, "手机");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.email, "邮箱");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.area, "熟悉的业务内容");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.profession, "所属行业");
                excel.AddMapping<MIS_ProfessorOuterModel>(r => r.office, "办公地址");

                var excelContent = excel.WorksheetRange<MIS_ProfessorOuterModel>("A2", "K100", "2016届");

                int rowIndex = 1;
                foreach (var row in excelContent)
                {
                    var errorMessage = new StringBuilder();
                    var professor = new MIS_ProfessorOuterModel();
                    professor.Id = row.Id;
                    professor.uid = row.uid;
                    professor.name = row.name;
                    professor.sex = row.sex;
                    professor.position = row.position;
                    professor.department = row.department;
                    professor.mobile = row.mobile;
                    professor.email = row.email;
                    professor.area = row.area;
                    professor.profession = row.profession;
                    professor.office = row.office;
                    if (professor.uid == null)
                    {
                        errorMessage.Append("证书编号 - 不能为空");

                    }
                    if (errorMessage.Length > 0)
                    {
                        errors.add(string.Format("第{0}行发现错误 :{1}{2}", rowIndex, errorMessage.ToString(), "<br />"));
                    }
                    else
                    {
                        modelList.Add(professor);
                    }
                    rowIndex++;
                }
                if(errors.Count()>0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }
        public void SaveImportData(IEnumerable<MIS_ProfessorOuterModel> modelList)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach(var model in modelList)
                    {
                        MIS_ProfessorOuter entity = new MIS_ProfessorOuter();
                        entity.Id = model.Id;
                        entity.uid = model.uid;
                        entity.name = model.name;
                        entity.sex = model.sex;
                        entity.position = model.position;
                        entity.department = model.department;
                        entity.mobile = model.mobile;
                        entity.email = model.email;
                        entity.area = model.area;
                        entity.profession = model.profession;
                        entity.office = model.office;
                        entity.stuNumPG = model.stuNumPG;
                        entity.referee = model.referee;
                        entity.location = model.location;
                        entity.Account = model.Account;
                        db.Set<MIS_ProfessorOuter>().Add(entity);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
