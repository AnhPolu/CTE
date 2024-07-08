using CTEMS.Infrastructure;
using CTEMS.Service.DTO;
using CTEMS.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTEMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        private IEmployeeService _eployeeService;
        private ICurrentUserService _currentUserService;
        public EmployeesController(IEmployeeService eployeeService, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _eployeeService = eployeeService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM employee)
        {
            try
            {
                return Ok(_eployeeService.Add(employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fail to create user");
            }
        }
    }
}
