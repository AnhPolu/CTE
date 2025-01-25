using CTEMS.Infrastructure;
using CTEMS.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTEMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        private IUserService _userService;
        private ICurrentUserService _currentUserService;
        public StudentsController(IUserService userService, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }
    }
}
