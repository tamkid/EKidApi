using EKidApi.RequestData.Vob;
using EKidApi.ResponseData;
using EKidApi.ResponseData.Vob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi.Services.Interfaces
{
    public interface IVobService
    {
        Task<Response_Vob_GetAll> GetAll(int limit, int page, string search);
        Task<Response_Vob_GetById> GetById(Guid id);
        Task<Response_Vob_Add> AddNew(Request_Vob_Add request);
        Task<Response_Vob_Update> Update(Request_Vob_Update request);
        Task<ResponseBase> Delete(Guid id);
    }
}
