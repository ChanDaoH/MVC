using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;

namespace Apps.IBLL
{
    public interface ISysUserBLL
    {
        List<PermModel> GetPermission(string accountId, string controller);
    }
}
