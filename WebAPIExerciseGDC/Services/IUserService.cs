using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIExerciseGDC.Dtos;

namespace WebAPIExerciseGDC.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDataDto>>> GetAllUserDetails();
        Task<ServiceResponse<GetUserDataDto>> GetUserDetailsById(int id);
        Task<ServiceResponse<GetUserDataDto>> AddNewUser(AddUserDataDto userDetailsModel);
        Task<ServiceResponse<GetUserDataDto>> UpdateUserDetails(UpdateUserDataDto userDetailsModel);
        Task<ServiceResponse<List<GetUserDataDto>>> DeleteUserDetails(int id);
    }
}