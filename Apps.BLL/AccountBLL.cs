using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Microsoft.Practices.Unity;

namespace Apps.BLL
{
    public class AccountBLL:IAccountBLL
    {
        [Dependency]
        public IAccountRepository Rep { get; set; }
        public SysUser Login(string username, string pwd)
        {
            return Rep.Login(username, pwd);
        }
    }
}
