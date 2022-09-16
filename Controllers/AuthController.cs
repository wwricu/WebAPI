using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ExpandoObject Post([FromForm] string Email, [FromForm] string PasswordHash)
        {
            dynamic response = new ExpandoObject();
            try
            {
                SysUser User = AuthenticationService.Login(Email, PasswordHash);
                HttpContext.Session.SetInt32("Permission", User.Permission);
                response.memberNumber = User.MemberNumber;
                response.memberName = new string[]
                {
                    User.Firstname,
                    User.Middlename,
                    User.Lastname,
                };
                response.Permission = User.Permission;
                response.status = "success";
            }
            catch (Exception e)
            {
                response.status = "failure";
                response.msg = e.Message;
            }

            return response;
        }

    }
    class LoginModel
    {
        public string UserEmail { get; set; }
        public string PasswordHash { get; set; }
    }
}
