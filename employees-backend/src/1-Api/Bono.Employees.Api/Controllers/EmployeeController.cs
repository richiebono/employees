using Bono.Employees.Api.Filters;
using Bono.Employees.Application.Interfaces;
using Bono.Employees.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Bono.Employees.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeController : BaseController
    {

        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            this.employeeService = EmployeeService;
        }

        [HttpGet]
        [ResponseHeader("Access-Control-Expose-Headers", "Content-Range")]
        public IActionResult Get([FromQuery] FilterViewModel filterRequest)
        {
            var Employees = this.employeeService.Filter(filterRequest);
            return OkFilter(Employees, "Employees", filterRequest.start, filterRequest.size, this.employeeService.Count(filterRequest));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(this.employeeService.GetAll());
        }

        [HttpPost]
        public IActionResult Post(EmployeeViewModel EmployeeViewModelViewModel)
        {
            try
            {
                EmployeeViewModelViewModel.UserId = UserId();
                var result = this.employeeService.Post(EmployeeViewModelViewModel);

                if (result.Errors.Any())
                    return BadRequest(result);

                return Ok(this.employeeService.Post(EmployeeViewModelViewModel).Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(this.employeeService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, EmployeeViewModel EmployeeViewModelViewModel)
        {
            try
            {
                var result = this.employeeService.Put(EmployeeViewModelViewModel);

                if (result.Errors.Any())
                    return BadRequest(result);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(this.employeeService.Delete(id));
        }

    }
}