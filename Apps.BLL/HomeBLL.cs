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
    public class HomeBLL : IHomeBLL
    {
        [Dependency]
        public IHomeRepository Rep { get; set; }
        public List<SysModule> GetMenuByPersonId(string moduleId)
        {
            return Rep.GetMenuByPersonId(moduleId);
        }
    }
}
