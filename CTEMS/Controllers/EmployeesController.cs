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

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeVM>> List(EmployeeVM employee)
        {
            try
            {
                return Ok( _eployeeService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Fail to create user");
            }
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeVM>> Employee(int id)
        {
            try
            {
                return Ok(await _eployeeService.GetById(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Fail to create user");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employee)
        {
            try
            {
                return Ok(await _eployeeService.Add(employee));
            }
            catch (Exception)
            {
                return StatusCode(500, "Fail to create user");
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update(int id, EmployeeVM employee)
        {
            try
            {
                return Ok(await _eployeeService.Update(employee));
            }
            catch (Exception)
            {
                return StatusCode(500, "Fail to create user");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _eployeeService.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Fail to create user");
            }
        }
    }
}
