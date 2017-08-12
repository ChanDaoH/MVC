using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apps.BLL;
using Apps.DAL;
using Apps.IBLL;
using Apps.IDAL;
using Microsoft.Practices.Unity;

using Apps.MIS.IBLL;
using Apps.MIS.BLL;
using Apps.MIS.IDAL;
using Apps.MIS.DAL;

using Apps.Flow.IDAL;
using Apps.Flow.DAL;
using Apps.Flow.IBLL;
using Apps.Flow.BLL;

namespace Apps.Core
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<ISysSampleBLL, SysSampleBLL>();//样例
            container.RegisterType<ISysSampleRepository, SysSampleRepository>();

            container.RegisterType<IHomeBLL, HomeBLL>();
            container.RegisterType<IHomeRepository, HomeRepository>();

            container.RegisterType<ISysLogBLL, SysLogBLL>();
            container.RegisterType<ISysLogRepository, SysLogRepository>();

            container.RegisterType<ISysExceptionBLL, SysExceptionBLL>();
            container.RegisterType<ISysExceptionRepository, SysExceptionRepository>();

            container.RegisterType<IAccountBLL, AccountBLL>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<ISysUserBLL, SysUserBLL>();
            container.RegisterType<ISysRightRepository, SysRightRepository>();
            container.RegisterType<ISysUserRepository, SysUserRepository>();

            container.RegisterType<ISysModuleBLL, SysModuleBLL>();
            container.RegisterType<ISysModuleRepository, SysModuleRepository>();

            container.RegisterType<ISysModuleOperateBLL, SysModuleOperateBLL>();
            container.RegisterType<ISysModuleOperateRepository, SysModuleOperateRepository>();

            container.RegisterType<ISysRoleBLL, SysRoleBLL>();
            container.RegisterType<ISysRoleRepository, SysRoleRepository>();

            container.RegisterType<ISysRightBLL, SysRightBLL>();

            container.RegisterType<IMIS_ArticleBLL, MIS_ArticleBLL>();
            container.RegisterType<IMIS_ArticleRepository, MIS_ArticleRepository>();

            container.RegisterType<IMIS_Article_CategoryBLL, MIS_Article_CategoryBLL>();
            container.RegisterType<IMIS_Article_CategoryRepository, MIS_Article_CategoryRepository>();

            //excel
            container.RegisterType<IMIS_PersonBLL, MIS_PersonBLL>();
            container.RegisterType<IMIS_PersonRepository, MIS_PersonRepository>();
            container.RegisterType<IMIS_ProfessorOuterBLL, MIS_ProfessorOuterBLL>();
            container.RegisterType<IMIS_ProfessorOuterRepository, MIS_ProfessorOuterRepository>();

            //SysStruct
            container.RegisterType<ISysStructBLL, SysStructBLL>();
            container.RegisterType<ISysStructRepository, SysStructRepository>();
        }
    }
}