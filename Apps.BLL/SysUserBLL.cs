using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Apps.Models;
using Apps.Models.Sys;
using Apps.IBLL;
using Apps.IDAL;
using Microsoft.Practices.Unity;
using Apps.Common;
using Apps.BLL.Core;

namespace Apps.BLL
{
    public class SysUserBLL: BaseBLL, ISysUserBLL
    {
        [Dependency]
        public ISysRightRepository Rep { get; set; }

        [Dependency]
        public ISysUserRepository m_Rep { get; set; }

        public List<PermModel> GetPermission(string accountId, string controller)
        {
            return Rep.GetPermission(accountId, controller);
        }

        public List<SysUserModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysUser> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(a => a.UserName.Contains(queryStr) || a.TrueName.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList();
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<SysUserModel> CreateModelList(ref IQueryable<SysUser> queryData)
        {

            List<SysUserModel> modelList = (from r in queryData
                                            select new SysUserModel
                                            {
                                                Id = r.Id,
                                                UserName = r.UserName,
                                                Password = r.Password,
                                                TrueName = r.TrueName,
                                                Card = r.Card,
                                                MobileNumber = r.MobileNumber,
                                                PhoneNumber = r.PhoneNumber,
                                                QQ = r.QQ,
                                                EmailAddress = r.EmailAddress,
                                                OtherContact = r.OtherContact,
                                                Province = r.Province,
                                                City = r.City,
                                                Village = r.Village,
                                                Address = r.Address,
                                                State = (bool)r.State,
                                                CreateTime = (DateTime)r.CreateTime,
                                                CreatePerson = r.CreatePerson,
                                                Sex = r.Sex,
                                                Birthday = (DateTime)r.Birthday,
                                                JoinDate = (DateTime)r.JoinDate,
                                                Marital = r.Marital,
                                                Political = r.Political,
                                                Nationality = r.Nationality,
                                                Native = r.Native,
                                                School = r.School,
                                                Professional = r.Professional,
                                                Degree = r.Degree,
                                                DepId = r.DepId,
                                                PosId = r.PosId,
                                                Expertise = r.Expertise,
                                                JobState = r.JobState,
                                                Photo = r.Photo,
                                                Attach = r.Attach,
                                                Roles = (from a in r.SysRole
                                                         select a.Name).ToList()
                                            }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, SysUserModel model)
        {
            try
            {
                SysUser entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysUser();
                entity.Id = model.Id;
                entity.UserName = model.UserName;
                entity.Password = model.Password.MD5(); //md5加密
                entity.TrueName = model.TrueName;
                entity.Card = model.Card;
                entity.MobileNumber = model.MobileNumber;
                entity.PhoneNumber = model.PhoneNumber;
                entity.QQ = model.QQ;
                entity.EmailAddress = model.EmailAddress;
                entity.OtherContact = model.OtherContact;
                entity.Province = model.Province;
                entity.City = model.City;
                entity.Village = model.Village;
                entity.Address = model.Address;
                entity.State = model.State;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;
                entity.Sex = model.Sex;
                entity.Birthday = model.Birthday;
                entity.JoinDate = model.JoinDate;
                entity.Marital = model.Marital;
                entity.Political = model.Political;
                entity.Nationality = model.Nationality;
                entity.Native = model.Native;
                entity.School = model.School;
                entity.Professional = model.Professional;
                entity.Degree = model.Degree;
                entity.DepId = model.DepId;
                entity.PosId = model.PosId;
                entity.Expertise = model.Expertise;
                entity.JobState = model.JobState;
                entity.Photo = model.Photo;
                entity.Attach = model.Attach;
                if (m_Rep.Create(entity))
                {
                    return true;
                }
                else
                {
                    errors.add(Suggestion.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }

        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (m_Rep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    errors.add(Suggestion.DeleteFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }

        public bool Edit(ref ValidationErrors errors, SysUserModel model)
        {
            try
            {
                SysUser entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.add(Suggestion.Disable);
                    return false;
                }
                entity.Id = model.Id;
                entity.UserName = model.UserName;
                entity.Password = model.Password;
                entity.TrueName = model.TrueName;
                entity.Card = model.Card;
                entity.MobileNumber = model.MobileNumber;
                entity.PhoneNumber = model.PhoneNumber;
                entity.QQ = model.QQ;
                entity.EmailAddress = model.EmailAddress;
                entity.OtherContact = model.OtherContact;
                entity.Province = model.Province;
                entity.City = model.City;
                entity.Village = model.Village;
                entity.Address = model.Address;
                entity.State = model.State;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;
                entity.Sex = model.Sex;
                entity.Birthday = model.Birthday;
                entity.JoinDate = model.JoinDate;
                entity.Marital = model.Marital;
                entity.Political = model.Political;
                entity.Nationality = model.Nationality;
                entity.Native = model.Native;
                entity.School = model.School;
                entity.Professional = model.Professional;
                entity.Degree = model.Degree;
                entity.DepId = model.DepId;
                entity.PosId = model.PosId;
                entity.Expertise = model.Expertise;
                entity.JobState = model.JobState;
                entity.Photo = model.Photo;
                entity.Attach = model.Attach;

                if (m_Rep.Edit(entity))
                {
                    return true;
                }
                else
                {
                    errors.add(Suggestion.EditFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }

        public bool IsExist(string id)
        {
            return m_Rep.IsExist(id);
        }

        public SysUserModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysUser entity = m_Rep.GetById(id);
                SysUserModel model = new SysUserModel();
                model.Id = entity.Id;
                model.UserName = entity.UserName;
                model.Password = entity.Password;
                model.TrueName = entity.TrueName;
                model.Card = entity.Card;
                model.MobileNumber = entity.MobileNumber;
                model.PhoneNumber = entity.PhoneNumber;
                model.QQ = entity.QQ;
                model.EmailAddress = entity.EmailAddress;
                model.OtherContact = entity.OtherContact;
                model.Province = entity.Province;
                model.City = entity.City;
                model.Village = entity.Village;
                model.Address = entity.Address;
                model.State = (bool)entity.State;
                model.CreateTime = (DateTime)entity.CreateTime;
                model.CreatePerson = entity.CreatePerson;
                model.Sex = entity.Sex;
                model.Birthday = (DateTime)entity.Birthday;
                model.JoinDate = (DateTime)entity.JoinDate;
                model.Marital = entity.Marital;
                model.Political = entity.Political;
                model.Nationality = entity.Nationality;
                model.Native = entity.Native;
                model.School = entity.School;
                model.Professional = entity.Professional;
                model.Degree = entity.Degree;
                model.DepId = entity.DepId;
                model.PosId = entity.PosId;
                model.Expertise = entity.Expertise;
                model.JobState = entity.JobState;
                model.Photo = entity.Photo;
                model.Attach = entity.Attach;
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据用户Id获取角色详情
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserRoleInfoModel> GetRoleByUserId(ref GridPager pager, string id)
        {
            var roles = m_Rep.GetRoleByUserId(db, id);
            pager.totalRows = roles.Count();
            IQueryable<UserRoleInfoModel> queryData = roles.AsQueryable();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<UserRoleInfoModel> modelList = (from r in queryData
                                                 select new UserRoleInfoModel()
                                                 {
                                                     Id = r.Id,
                                                     Name = r.Name,
                                                     Description = r.Description,
                                                     CreateTime = r.CreateTime,
                                                     CreatePerson = r.CreatePerson,
                                                     Flag = r.Flag
                                                 }).ToList();
            return modelList;
        }
        /// <summary>
        /// 更新角色用户表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public bool UpdateSysRoleSysUser(ref ValidationErrors errors ,string userId, string[] roleIds)
        {
            try
            {
                m_Rep.UpdateSysRoleSysUser(userId, roleIds);
                return true;
            }
            catch(Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }
    }
}