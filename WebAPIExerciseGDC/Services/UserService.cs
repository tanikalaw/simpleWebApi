using AutoMapper;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIExerciseGDC.Data;
using WebAPIExerciseGDC.Dtos;


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

        public async Task<ServiceResponse<List<GetUserDataDto>>> GetAllUserDetails()
        {
            ServiceResponse<List<GetUserDataDto>> serviceResponse = new ServiceResponse<List<GetUserDataDto>>();

            List<Account> dbUsers = await _dataContext.UserData.ToListAsync();
            serviceResponse.Data = (dbUsers.Select(u => _mapper.Map<GetUserDataDto>(u))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDataDto>> GetUserDetailsById(int id)
        {
            ServiceResponse<GetUserDataDto> serviceResponse = new ServiceResponse<GetUserDataDto>();
            Account dbUser = await _dataContext.UserData.FirstOrDefaultAsync(x => x.Id == id);

            serviceResponse.Data = _mapper.Map<GetUserDataDto>(dbUser);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDataDto>> AddNewUser(AddUserDataDto userDetailsModel)
        {
            ServiceResponse<GetUserDataDto> serviceResponse = new ServiceResponse<GetUserDataDto>();

            Account user = _mapper.Map<Account>(userDetailsModel);

            //user.Id = users.Max(x => x.Id) + 1;
            await _dataContext.UserData.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            serviceResponse.Message = "Data Successfully Added!";
            serviceResponse.Data =  _mapper.Map<GetUserDataDto>(user);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDataDto>> UpdateUserDetails(UpdateUserDataDto updateUserDetailsDto)
        {
            ServiceResponse<GetUserDataDto> serviceResponse = new ServiceResponse<GetUserDataDto>();

            try
            {
               var user = _mapper.Map<UpdateUserDataDto, Account>(updateUserDetailsDto);
                 _dataContext.UserData.Update(user);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Message = "User Date Updated!";
                serviceResponse.Data = _mapper.Map<GetUserDataDto>(user);
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
                Account user = await _dataContext.UserData.FirstAsync(x => x.Id == id);
                _dataContext.UserData.Remove(user);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Message = "Deleted Data!";
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
