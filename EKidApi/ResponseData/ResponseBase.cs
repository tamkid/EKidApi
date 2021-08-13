using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi.ResponseData
{
    public class ResponseBase
    {
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }

        public ResponseBase()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
        }
    }
}
