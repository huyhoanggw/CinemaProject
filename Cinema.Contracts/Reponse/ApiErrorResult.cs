using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Contracts.Reponse
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public List<string> Errors { set; get; }
        public ApiErrorResult()
        {
            
        }
        public ApiErrorResult(string message ) : base(false , message)
        {
            
        }
        public ApiErrorResult(List<string> errors) : base(false)
        {
            Errors = errors;
        }
    }
}
