using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Common
{
    public class GridPager
    {
        public int rows { get; set; }//每页行数
        public int page { get; set; }//当前页是第几页
        public string order { get; set; }//排序方式
        public string sort { get; set; }//排序列
        public int totalRows { get; set; }//总行数

        public int totalPages //总页数
        {
            get
            {
                return (int)Math.Ceiling((float)totalRows / (float)rows);
            }
        }

    }
}
