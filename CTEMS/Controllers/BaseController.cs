using CTEMS.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;


namespace CTEMS.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController(ICurrentUserService _currentUserService)
        {
           _currentUserService.CurrentUserId = Int32.Parse(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
