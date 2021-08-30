using AutoMapper;
using EKidApi.EF;
using EKidApi.Models;
using EKidApi.Repository;
using EKidApi.RequestData.Vob;
using EKidApi.ResponseData;
using EKidApi.ResponseData.Vob;
using EKidApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EKidApi.Services
{
    public class VobService : IVobService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VobService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response_Vob_GetAll> GetAll(int limit, int page, string search)
        {
            Response_Vob_GetAll response = new Response_Vob_GetAll();

            bool isAll = limit <= 0;

            try
            {
                Expression<Func<Vob, bool>> filter = null;
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.Trim().ToLower();
                    filter = vob => vob.Word.Trim().ToLower().Contains(search) || vob.Meaning.Trim().ToLower().Contains(search);
                }

                var query = _unitOfWork.Repository<Vob>()
                                .GetQuery( filter: filter
                                         , orderBy: q => q.OrderBy(s => s.Word));

                var total = query.Count();

                List<Vob> lstEnt;
                if (!isAll)
                {
                    lstEnt = await query.Skip(page * limit).Take(limit).ToListAsync();
                }
                else
                {
                    lstEnt = await query.ToListAsync();
                }

                var lstData = _mapper.Map<List<VobModel>>(lstEnt);

                var responseData = new ResponseData_Vob_GetAll()
                {
                    ListData = lstData,
                    Limit = limit,
                    Page = page,
                    Total = total
                };
                response.Data = responseData;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }

        public async Task<Response_Vob_GetById> GetById(Guid id)
        {
            Response_Vob_GetById response = new Response_Vob_GetById();

            try
            {
                ResponseData_Vob_GetById responseData = new ResponseData_Vob_GetById();
                var ent = await _unitOfWork.Repository<Vob>().GetByID_Async(id);
                if (ent != null)
                {
                    responseData.Data = _mapper.Map<VobModel>(ent);
                    response.Data = responseData;
                }
                else
                {
                    response.IsOk = false;
                    response.ErrorMessage = "Unable to find the object";
                }
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }

        public async Task<Response_Vob_Add> AddNew(Request_Vob_Add request)
        {
            Response_Vob_Add response = new Response_Vob_Add();

            try
            {
                GenericRepo<Vob> repo = _unitOfWork.Repository<Vob>();
                var existed = await repo.GetQuery(filter: o => o.Word == request.Word.Trim().ToLower() && o.WordType == request.WordType)
                                        .FirstOrDefaultAsync();
                if (existed != null)
                {
                    response.IsOk = false;
                    response.ErrorMessage = "Duplicate data";
                    return response;
                }

                var entity = _mapper.Map<Vob>(request);
                repo.Insert(entity);
                await _unitOfWork.Save_Async();

                response.id = entity.Id;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }

        public async Task<Response_Vob_Update> Update(Request_Vob_Update request)
        {
            Response_Vob_Update response = new Response_Vob_Update();

            try
            {
                GenericRepo<Vob> repo = _unitOfWork.Repository<Vob>();

                var entity = await repo.GetQuery(filter: o => o.Id == request.Id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    response.IsOk = false;
                    response.ErrorMessage = "Unable to find the word";
                    return response;
                }

                var existed = await repo.GetQuery(filter: o => o.Word == request.Word.Trim().ToLower() && o.WordType == request.WordType && o.Id != entity.Id)
                                        .FirstOrDefaultAsync();
                if (existed != null)
                {
                    response.IsOk = false;
                    response.ErrorMessage = "Duplicate data";
                    return response;
                }

                entity.Word = request.Word.Trim().ToLower();
                entity.WordType = request.WordType;
                entity.Spelling = request.Spelling;
                entity.Meaning = request.Meaning;
                entity.Example = request.Example;
                await _unitOfWork.Save_Async();

                response.id = entity.Id;
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }

        public async Task<ResponseBase> Delete(Guid id)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                GenericRepo<Vob> repo = _unitOfWork.Repository<Vob>();

                var entity = await repo.GetQuery(filter: o => o.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    response.IsOk = false;
                    response.ErrorMessage = "Unable to find the word";
                    return response;
                }

                repo.Delete(id);
                await _unitOfWork.Save_Async();
            }
            catch (Exception ex)
            {
                response.IsOk = false;
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }
    }
}
