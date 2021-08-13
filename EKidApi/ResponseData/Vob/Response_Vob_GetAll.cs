using EKidApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi.ResponseData.Vob
{
    public class Response_Vob_GetAll: ResponseBase
    {
        public ResponseData_Vob_GetAll Data { get; set; }
    }

    public class ResponseData_Vob_GetAll 
    {
        public List<VobModel> ListData { get; set; }        
        public int Limit { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
    }
}
