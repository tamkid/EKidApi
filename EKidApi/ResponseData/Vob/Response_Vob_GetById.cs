using EKidApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi.ResponseData.Vob
{
    public class Response_Vob_GetById : ResponseBase
    {
        public ResponseData_Vob_GetById Data { get; set; }
    }

    public class ResponseData_Vob_GetById
    {
        public VobModel Data { get; set; }        
    }
}
