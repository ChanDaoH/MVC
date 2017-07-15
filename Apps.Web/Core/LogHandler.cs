using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.DAL;
using Apps.Models;
using Microsoft.Practices.Unity;
using Apps.Common;

namespace Apps.Web.Core
{
    public class LogHandler
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="oper">操作人</param>
        /// <param name="mes">操作信息</param>
        /// <param name="result">结果</param>
        /// <param name="type">类型</param>
        /// <param name="module">操作模块</param>
        public static void WriteServiceLog(string oper, string mes, string result, string type, string module)
        {
            SysLog entity = new SysLog();
            SysLogRepository Rep = new SysLogRepository(new DBContainer());
            entity.Id = ResultHelper.NewId;
            entity.Operator = oper;
            entity.Message = mes;
            entity.Result = result;
            entity.Type = type;
            entity.Module = module;
            entity.CreateTime = ResultHelper.NowTime;
            Rep.Create(entity);
        }
    }
}
