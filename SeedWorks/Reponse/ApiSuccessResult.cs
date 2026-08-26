using SeedWorks.ApiReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedWorks.Reponse
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult()
        {
            
        }
        public ApiSuccessResult(string message) : base(true , message)
        {
            
        }
        public ApiSuccessResult(T obj , string message) : base(true,obj , message) 
        {
            
        }
    }
}
