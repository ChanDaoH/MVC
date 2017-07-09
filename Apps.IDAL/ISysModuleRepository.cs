using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;

namespace Apps.IDAL
{
    public interface ISysModuleRepository
    {
        IQueryable<SysModule> GetList(DBContainer db);
        IQueryable<SysModule> GetModuleBySystem(DBContainer db, string parentId);
        int Create(SysModule entity);
        void Delete(DBContainer db, string id);
        int Edit(SysModule entity);
        SysModule GetById(string id);
        bool IsExist(string id);
    }
}
