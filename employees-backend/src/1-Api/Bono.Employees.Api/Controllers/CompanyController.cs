using Bono.Employees.Api.Controllers;
using Bono.Employees.Api.Filters;
using Bono.Employees.Application.Interfaces;
using Bono.Employees.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Bono.Companys.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CompanyController : BaseController
    {

        private readonly ICompanyService CompanyService;

        public CompanyController(ICompanyService CompanyService)
        {
            this.CompanyService = CompanyService;
        }

        [HttpGet]
        [ResponseHeader("Access-Control-Expose-Headers", "Content-Range")]
        public IActionResult Get([FromQuery] FilterViewModel filterRequest)
        {
            var Companys = this.CompanyService.Filter(filterRequest);
            return OkFilter(Companys, "Companys", filterRequest.start, filterRequest.size, this.CompanyService.Count(filterRequest));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(this.CompanyService.GetAll());
        }

        [HttpPost]
        public IActionResult Post(CompanyViewModel CompanyViewModelViewModel)
        {
            try
            {
                CompanyViewModelViewModel.UserId = UserId();
                var result = this.CompanyService.Post(CompanyViewModelViewModel);

                if (result.Errors.Any())
                    return BadRequest(result);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(this.CompanyService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, CompanyViewModel CompanyViewModelViewModel)
        {
            try
            {
                var result = this.CompanyService.Put(CompanyViewModelViewModel);

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
            return Ok(this.CompanyService.Delete(id));
        }

    }
}