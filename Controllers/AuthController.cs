using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using WebAPI.Model;
using SqlSugar.Extensions;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Password([FromBody] SysUser Credential)
        {
            PublicInfoModel User = SessionService.GetSessionInfo(HttpContext.Session);
            string msg = "Session Success";

            if (User.Permission > 0
                && User.UserNumber == Credential.UserNumber)
            {
                Debug.WriteLine(User.UserName);
                goto SuccessLogin;
            }
            try
            {
                User = AuthenticationService.Login(Credential.UserNumber, Credential.PasswordHash);
                SessionService.SetSessionInfo(HttpContext.Session, User);
                msg = "Password success";
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
        SuccessLogin:
            PublicInfoModel PublicInfo = new PublicInfoModel()
            {
                UserName = User.UserName,
                UserNumber = User.UserNumber,
                Email = User.Email,
                Permission = User.Permission,
            };
            return new SuccessResponseModel()
            {
                Message = msg,
                obj = new object[2]
                {
                    PublicInfo,
                    SecurityService.CreateJWT(PublicInfo),
                }
            };
        }
        public ResponseModel Token([FromBody] string token)
        {
            try
            {
                PublicInfoModel PublicInfo = SecurityService.ValidateJWT(token);
                SessionService.SetSessionInfo(HttpContext.Session,PublicInfo);
                return new SuccessResponseModel()
                {
                    Message = "Success Token Login",
                    obj = new object[2]
                    {
                        PublicInfo,
                        SecurityService.CreateJWT(PublicInfo),
                    }
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

        [HttpGet]
        public void Logout()
        {
            HttpContext.Session.Clear();
        }
    }

}

