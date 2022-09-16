using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using WebAPI.Model;
using SqlSugar.Extensions;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public void SetSessionInfo(PrivateInfoModel UserInfo)
        {
            HttpContext.Session.SetInt32("Permission", UserInfo.Permission);
            HttpContext.Session.SetString("Email", UserInfo.Email);

        }
        [HttpPost]
        public ResponseModel Post([FromBody] PasswordLoginModel Credential)
        {
            try
            {
                SysUser User = AuthenticationService.Login(Credential.Email, Credential.PasswordHash);
                HttpContext.Session.SetInt32("Permission", User.Permission);
                return new SuccessResponseModel()
                {
                    Message = "Success Login",
                    obj = new PublicInfoModel()
                    {
                        UserName = User.UserName,
                        UserNumber = User.UserNumber,
                        Email = User.Email,
                        Permission = User.Permission,
                    },
                };
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
        }
    }

}

