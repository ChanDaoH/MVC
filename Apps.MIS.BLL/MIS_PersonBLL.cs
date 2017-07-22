using Apps.BLL.Core;
using Apps.Common;
using Apps.Models;
using Apps.Models.Sys;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MIS.BLL
{
    public partial class MIS_PersonBLL
    {
        public bool CheckImportData(string fileName, List<MIS_PersonModel> personList, ref ValidationErrors errors)
        {
            try
            {
                var targetFile = new FileInfo(fileName);
                if (!targetFile.Exists)
                {
                    errors.add("文件不存在");
                    return false;
                }
                var excelFile = new ExcelQueryFactory(fileName);
                //对应表头
                excelFile.AddMapping<MIS_PersonModel>(x => x.Name, "Name");
                excelFile.AddMapping<MIS_PersonModel>(x => x.Sex, "Sex");
                excelFile.AddMapping<MIS_PersonModel>(x => x.Age, "Age");
                excelFile.AddMapping<MIS_PersonModel>(x => x.IDCard, "IDCard");
                excelFile.AddMapping<MIS_PersonModel>(x => x.Phone, "Phone");
                excelFile.AddMapping<MIS_PersonModel>(x => x.Email, "Email");
                excelFile.AddMapping<MIS_PersonModel>(x => x.Address, "Address");
                excelFile.AddMapping<MIS_PersonModel>(x => x.Region, "Region");
                excelFile.AddMapping<MIS_PersonModel>(x => x.Category, "Category");
                //sheetName
                var excelContent = excelFile.Worksheet<MIS_PersonModel>(0);
                int rowIndex = 1;
                //检查数据正确性
                foreach (var row in excelContent)
                {
                    var errorMessage = new StringBuilder();
                    var person = new MIS_PersonModel();
                    person.Id = row.Id;
                    person.Name = row.Name;
                    person.Sex = row.Sex;
                    person.Age = row.Age;
                    person.IDCard = row.IDCard;
                    person.Phone = row.Phone;
                    person.Email = row.Email;
                    person.Address = row.Address;
                    person.Region = row.Region;
                    person.Category = row.Category;
                    if (string.IsNullOrEmpty(person.Name))
                    {
                        errorMessage.Append("Name - 不能为空. ");
                    }
                    if (string.IsNullOrWhiteSpace(row.IDCard))
                    {
                        errorMessage.Append("IDCard - 不能为空. ");
                    }
                    if (errorMessage.Length > 0)
                    {
                        errors.add(string.Format("第{0}行发现错误:{1}{2}", rowIndex, errorMessage, "<br />"));
                    }
                    else
                    {
                        personList.Add(person);
                    }
                    rowIndex++;
                }
                if (errors.Count()>0)
                    return false;
                else
                    return true;
            }
            catch(Exception ex)
            {
                errors.add("数据表列名不合法");
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }
        public void SaveImportData(IEnumerable<MIS_PersonModel> personList)
        {
            try
            {
                DBContainer db = new DBContainer();
                foreach (var model in personList)
                {
                    MIS_Person entity = new MIS_Person();
                    entity.Id = ResultHelper.NewId;
                    entity.Name = model.Name;
                    entity.Sex = model.Sex;
                    entity.Age = model.Age;
                    entity.IDCard = model.IDCard;
                    entity.Phone = model.Phone;
                    entity.Email = model.Email;
                    entity.Address = model.Address;
                    entity.CreateTime = ResultHelper.NowTime;
                    entity.Region = model.Region;
                    entity.Category = model.Category;
                    db.MIS_Person.Add(entity);

                }
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
