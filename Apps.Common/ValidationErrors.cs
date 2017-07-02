using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Common
{
    public class ValidationErrors : List<ValidationError>
    {
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="errorMessage">信息描述</param>
        public void add(string errorMessage)
        {
            base.Add(new ValidationError { ErrorMessage = errorMessage });
        }
        /// <summary>
        /// 获取错误集合
        /// </summary>
        public string Error
        {
            get
            {
                string error = "";
                this.All(r =>
                {
                    error += r.ErrorMessage;
                    return true;
                });

                return error;
            }
        }

    }
}
