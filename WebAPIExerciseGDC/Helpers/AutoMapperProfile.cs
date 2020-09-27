using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIExerciseGDC.Dtos;
using WebAPIExerciseGDC.Model;

namespace WebAPIExerciseGDC
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDataModel, GetUserDataDto>();
            CreateMap<AddUserDataDto, UserDataModel>();
            CreateMap<UpdateUserDataDto, UserDataModel>();
        }
    }
}
