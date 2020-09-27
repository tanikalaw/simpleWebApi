﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIExerciseGDC.Dtos;
using WebAPIExerciseGDC.Model;

namespace WebAPIExerciseGDC.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDataDto>>> GetAllUserDetails();
        Task<ServiceResponse<GetUserDataDto>> GetUserDetailsById(int id);
        Task<ServiceResponse<List<GetUserDataDto>>> AddNewUser(AddUserDataDto userDetailsModel);
        Task<ServiceResponse<GetUserDataDto>> UpdateUserDetails(UpdateUserDataDto userDetailsModel);
        Task<ServiceResponse<List<GetUserDataDto>>> DeleteUserDetails(int id);
    }
}