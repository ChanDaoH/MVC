using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;

namespace Apps.IBLL
{
    public interface IHomeBLL
    {
        List<SysModule> GetMenuByPersonId(string moduleId);
    }
}
