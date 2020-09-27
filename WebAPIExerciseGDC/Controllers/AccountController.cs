using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIExerciseGDC.Dtos;
using WebAPIExerciseGDC.Services;

namespace WebAPIExerciseGDC.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/get/userdata/")]
        [HttpGet]
        public async Task<IActionResult> GetUserData()
        {
            ServiceResponse<List<GetUserDataDto>> response = await _userService.GetAllUserDetails();
            if (response.Data != null)
                return Ok(response);
            else
                return NotFound(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDataById([FromQuery]int id)
        {
            ServiceResponse<GetUserDataDto> response = await _userService.GetUserDetailsById(id);
            if (response.Data != null)
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [Route("api/add/userdata/")]
        [HttpPost]
        public async Task<IActionResult> AddUserData([FromBody] AddUserDataDto user)
        {
            ServiceResponse<GetUserDataDto> response = await _userService.AddNewUser(user);

            if (response.Data != null)
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [Route("api/update/userdata/")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataDto user)
        {
            ServiceResponse<GetUserDataDto> response = await _userService.UpdateUserDetails(user);
            if (response.Data == null)
                return BadRequest(response);
            else
                return Ok(response);
        }

        [Route("api/delete/userdata/")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserData([FromQuery] int id)
        {
            ServiceResponse<List<GetUserDataDto>> response = await _userService.DeleteUserDetails(id);
            if (response.Data == null)
                return BadRequest(response);
            else
                return Ok(response);
        }
    }
}
