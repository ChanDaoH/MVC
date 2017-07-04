using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.Models.Sys;
using Apps.IDAL;

namespace Apps.DAL
{
    public class SysRightRepository:ISysRightRepository,IDisposable
    {
        /// <summary>
        /// 根据用户Id和控制器获取用户操作权限
        /// </summary>
        /// <param name="accountId">accountId</param>
        /// <param name="controller">controller</param>
        /// <returns></returns>
        public List<PermModel> GetPermission (string accountId, string controller)
        {
            using (DBContainer db = new DBContainer() )
            {
                var rights = db.P_Sys_GetRightOperate2(accountId, controller);
                List < PermModel > permList = (from r in rights
                                           select new PermModel
                                      {
                                          KeyCode = r.KeyCode,
                                          IsValid = r.IsValid
                                      }).ToList();
                return permList;
            }
        }

        public void Dispose()
        {

        }
    }
}
