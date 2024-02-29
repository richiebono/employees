using Bono.Employees.Api.Filters;
using Bono.Employees.Application.Interfaces;
using Bono.Employees.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bono.Employees.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeTypeController : BaseController
    {

        private readonly IEmployeeTypeService employeeTypeService;

        public EmployeeTypeController(IEmployeeTypeService EmployeeTypeService)
        {
            this.employeeTypeService = EmployeeTypeService;
        }

        [HttpGet]
        [ResponseHeader("Access-Control-Expose-Headers", "Content-Range")]
        public IActionResult Get([FromQuery] FilterViewModel filterRequest)
        {
            var employeesType = this.employeeTypeService.Filter(filterRequest);
            return OkFilter(employeesType, "employeeTypes", filterRequest.start, filterRequest.size, this.employeeTypeService.Count(filterRequest));
        }        
    }
}