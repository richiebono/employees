using Bono.Employees.Application.Interfaces;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bono.Employees.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.userService.GetAll());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(this.userService.Post(userViewModel));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(this.userService.GetById(id));
        }

        [HttpPut]
        public IActionResult Put(UserViewModel userViewModel)
        {
            return Ok(this.userService.Put(userViewModel));
        }

        [HttpDelete]
        public IActionResult Delete(string userId)
        {
            return Ok(this.userService.Delete(userId));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserAuthenticateRequestViewModel userViewModel)
        {
            // var userViewModel = new UserAuthenticateRequestViewModel
            // {
            //     Email = username,
            //     Password = password
            // };

            try
            {
                var result = this.userService.Authenticate(userViewModel);

                if (result.IsValid)
                {
                    return Ok(result.Data);
                }
                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}