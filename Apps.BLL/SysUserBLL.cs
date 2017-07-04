using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Apps.Models.Sys;
using Apps.IBLL;
using Apps.IDAL;
using Microsoft.Practices.Unity;

namespace Apps.BLL
{
    public class SysUserBLL:ISysUserBLL
    {
        [Dependency]
        public ISysRightRepository Rep { get; set; }
        public List<PermModel> GetPermission(string accountId, string controller)
        {
            return Rep.GetPermission(accountId, controller);
        }
    }
}