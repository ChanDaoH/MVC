using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using Apps.Common;

namespace Apps.MIS.IBLL
{
    public partial interface IMIS_PersonBLL
    {
        bool CheckImportData(string fileName, List<MIS_PersonModel> personList, ref ValidationErrors errors);
        void SaveImportData(IEnumerable<MIS_PersonModel> personList);
    }
}
