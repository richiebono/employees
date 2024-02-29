using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bono.Employees.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string UserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;       
        protected IActionResult OkFilter<T>(T result, string name, int start, int size, int total)
        {
            if (result == null)
                return NotFound();

            HttpContext.Response.Headers.Add("Content-Range", name + " " + start + "-" + size + "/" + total);
            
            return Ok(result);
        }
    }
}
