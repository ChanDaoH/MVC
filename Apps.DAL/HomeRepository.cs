using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IDAL;
using Apps.Models;

namespace Apps.DAL
{
    public class HomeRepository : IHomeRepository,IDisposable
    {
        public List<SysModule> GetMenuByPersonId(string personId,string moduleId)
        {
            using (DBContainer db = new DBContainer())
            {
                /*
                 * var menus = (from m in db.SysModule
                             join r in db.SysRight on m.Id equals r.ModuleId
                             join role in
                             (
                                from role in db.SysRole
                                from u in role.SysUser
                                where u.Id == personId
                                select role
                             ) on r.RoleId equals role.Id
                             where r.Rightflag == true
                             where m.ParentId == moduleId
                             where m.Id != "0"
                             select m).Distinct().OrderBy(m => m.Sort).ToList();
                             */
                //测试，自己实现的
                var menus = (from m in db.SysModule
                             from right in db.SysRight
                             from role in db.SysRole
                             from user in role.SysUser
                             where m.Id == right.ModuleId
                             where m.ParentId == moduleId
                             where right.RoleId == role.Id
                             where user.Id == personId
                             where m.Id != "0"
                             where right.Rightflag == true
                             select m).Distinct().OrderBy(m => m.Sort).ToList();
                return menus;
            }
        }
        public void Dispose()
        {

        }
    }   
}
