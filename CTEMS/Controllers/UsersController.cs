using CTEMS.Infrastructure;
using CTEMS.Service.DTO;
using CTEMS.Service.Helpers;
using CTEMS.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CTEMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private IUserService _userService;
        private ICurrentUserService _currentUserService;
        public UsersController(IUserService userService, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public IActionResult Create(UserVM userRM)
        {
            try
            {
                return Ok(_userService.Add(userRM));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fail to create user");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public IActionResult Update(UserVM userRM)
        {
            try
            {
                return Ok( _userService.Update(userRM));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fail to update user");
            }
        }

        //        [HttpDelete]
        //        [Route("{id:int}")]
        //        public async Task<IActionResult> Delete(int id)
        //        {
        //            try
        //            {
        //                await _userService.Delete(id)
        //;
        //                return Ok();
        //            }
        //            catch (Exception ex)
        //            {
        //                return StatusCode(500, "Fail to delete user");
        //            }
        //        }

        [HttpPost]
        [Route("log-in")]
        public IActionResult LogIn(AuthenticateRequest request)
        {
            try
            {
                var token = _userService.Authenticate(request);
                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
