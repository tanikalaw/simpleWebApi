using Core.Entities;
using WebAPIExerciseGDC.Dtos;

namespace WebAPIExerciseGDC
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, GetUserDataDto>();
            CreateMap<AddUserDataDto, Account>();
            CreateMap<UpdateUserDataDto, Account>();
        }
    }
}
