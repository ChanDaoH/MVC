using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public class SysRightOperateModel
    {
        public string Id { get; set; }
        public string RightId { get; set; }
        public string KeyCode { get; set; }
        public bool IsValid { get; set; }
    }
}
