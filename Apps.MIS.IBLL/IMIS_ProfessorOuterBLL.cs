using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Common;
using Apps.Models.Sys;

namespace Apps.MIS.IBLL
{
    public partial interface IMIS_ProfessorOuterBLL
    {
        bool CheckImportData(string fileName, ref List<MIS_ProfessorOuterModel> modelList, ref ValidationErrors errors);
        void SaveImportData(IEnumerable<MIS_ProfessorOuterModel> modelList);
        List<MIS_ProfessorOuterModel> GetImportData(string fileName);
    }
}
