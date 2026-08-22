using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.ApiReponse
{
    public class ApiResult<T>
    {
       public  bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
        public ApiResult()
        {
            
        }
        public ApiResult(bool isSuccess , string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;

        }
        public ApiResult(bool isSuccess, T resultObj, string message = null)
        {
            ResultObj = resultObj;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
