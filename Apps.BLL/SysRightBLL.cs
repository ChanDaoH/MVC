using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.Models.Sys;
using Apps.BLL.Core;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Common;
using Microsoft.Practices.Unity;

namespace Apps.BLL
{
    public class SysRightBLL : BaseBLL,ISysRightBLL
    {
        //业务层注入
        [Dependency]
        public ISysRightRepository Rep { get; set; }

        public bool UpdateRight(ValidationErrors errors, SysRightOperateModel model)
        {
            try
            {
                if ( model != null )
                {
                    SysRightOperate entity = new SysRightOperate() {
                        Id = model.Id,
                        RightId = model.RightId,
                        KeyCode = model.KeyCode,
                        IsValid = model.IsValid
                    };
                    if ( Rep.UpdateRight(entity) )
                    {
                        return true;
                    }
                    else
                    {
                        errors.add(Suggestion.UpdateFail);
                        return false;
                    }
                }
                else
                {
                    errors.add(Suggestion.UpdateFail);
                    return false;
                }
            }
            catch(Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }
        public List<RightModuleOperateModel> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            return Rep.GetRightByRoleAndModule(roleId, moduleId);
        }
    }
}
