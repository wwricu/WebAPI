using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Add([FromBody] Privilege Role)
        {
            return new FailureResponseModel();
        }
    }
}
