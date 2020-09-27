using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebAPIExerciseGDC.Data;


namespace WebAPIExerciseGDC.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public UserService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        private static List<UserDataModel> users = new List<UserDataModel>()
        {
            new UserDataModel {Id = 0, FirstName="RJ", LastName="Samonte"}
        };

        public async Task<ServiceResponse<List<GetUserDataDto>>> GetAllUserDetails()
        {
            ServiceResponse<List<GetUserDataDto>> serviceResponse = new ServiceResponse<List<GetUserDataDto>>();

            List<UserDataModel> dbUsers = await _dataContext.UserData.ToListAsync();
            serviceResponse.Data = (dbUsers.Select(u => _mapper.Map<GetUserDataDto>(u))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDataDto>> GetUserDetailsById(int id)
        {
            ServiceResponse<GetUserDataDto> serviceResponse = new ServiceResponse<GetUserDataDto>();
            UserDataModel dbUser = await _dataContext.UserData.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDataDto>(dbUser);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDataDto>>> AddNewUser(AddUserDataDto userDetailsModel)
        {
            ServiceResponse<List<GetUserDataDto>> serviceResponse = new ServiceResponse<List<GetUserDataDto>>();

            UserDataModel user = _mapper.Map<UserDataModel>(userDetailsModel);

            //user.Id = users.Max(x => x.Id) + 1;
            await _dataContext.UserData.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            serviceResponse.Data = _dataContext.UserData.Select(u => _mapper.Map<GetUserDataDto>(u)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDataDto>> UpdateUserDetails(UpdateUserDataDto updateUserDetailsDto)
        {
            ServiceResponse<GetUserDataDto> serviceResponse = new ServiceResponse<GetUserDataDto>();

            try
            {
               var res = _mapper.Map<UpdateUserDataDto, UserDataModel>(updateUserDetailsDto);
                 _dataContext.UserData.Update(res);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUserDataDto>(res);
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDataDto>>> DeleteUserDetails(int id)
        {
            ServiceResponse<List<GetUserDataDto>> serviceResponse = new ServiceResponse<List<GetUserDataDto>>();
            try
            {
                UserDataModel user = await _dataContext.UserData.FirstAsync(x => x.Id == id);
                _dataContext.UserData.Remove(user);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = (_dataContext.UserData.Select(x => _mapper.Map<GetUserDataDto>(x))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
