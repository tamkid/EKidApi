using AutoMapper;
using EKidApi.EF;
using EKidApi.Models;
using EKidApi.RequestData.Vob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vob, VobModel>()
                .ForMember(destination => destination.WordTypeName,
                        options => options.MapFrom(source => clsUtil.GetEnumValue<Enums.EWorkType>(source.WordType)));
            CreateMap<Request_Vob_Add, Vob>()
                .ForMember(destination => destination.Word,
                        options => options.MapFrom(source => source.Word.Trim().ToLower()))
                .ForMember(destination => destination.Meaning,
                        options => options.MapFrom(source => source.Meaning.Trim().ToLower()))
                .ForMember(destination => destination.Id,
                        options => options.MapFrom(source => Guid.NewGuid()))
                .ForMember(destination => destination.CreatedDate,
                        options => options.MapFrom(source => DateTime.Now));
        }
    }
}
